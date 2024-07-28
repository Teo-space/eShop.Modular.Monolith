using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Params.CatalogService;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog.ProductList;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Services;

internal class CatalogProductTypeProducts(ICatalogDbContext catalogDbContext) : ICatalogProductTypeProducts
{
    private async Task<ProductType> GetProductType(int productTypeId)
    {
        return await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundApiException($"Not Found By Id {productTypeId}");
    }

    private async Task<IReadOnlyCollection<Product>> GetProducts(ProductParams param)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == param.ProductTypeId)
            .Include(x => x.Maker)
            .AsQueryable();

        if (param.Makers.Any())
        {
            productsQuery = productsQuery.Where(x => param.Makers.Contains(x.MakerId));
        }

        foreach (var filter in param.Params)
        {
            productsQuery = productsQuery
                .Where(p => p.ParamValues.Any(pv => filter.ParamId == pv.ParamId && filter.Values.Contains(pv.Value)));
        }

        int skip = ((param.PageNumber >=1 ? param.PageNumber : 1) - 1) * param.PageSize;

        return await productsQuery
            .OrderBy(x => x.Maker.Name).ThenBy(x => x.Name)
            .Skip(skip).Take(param.PageSize)
            .ToArrayAsync();
    }

    private IReadOnlyCollection<ProductModel> MapProducts(IReadOnlyCollection<Product> products)
    {
        return products
        .Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            MakerId = x.MakerId,
            MakerName = x.Maker.Name,
            Number = x.Number,
            Name = x.Name,
            Description = x.Description,
        })
        .OrderBy(x => x.MakerName).ThenBy(x => x.Name)
        .ToArray();
    }


    private async Task<IReadOnlyCollection<Maker>> GetMakers(int productTypeId,
        IReadOnlyCollection<int> makers,
        IReadOnlyCollection<ProductFilterParam> paramValues)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .Include(x => x.Maker)
            .AsQueryable();

        if (makers.Any())
        {
            productsQuery = productsQuery.Where(x => makers.Contains(x.MakerId));
        }

        foreach (var filter in paramValues)
        {
            productsQuery = productsQuery
                .Where(p => p.ParamValues.Any(pv => filter.ParamId == pv.ParamId && filter.Values.Contains(pv.Value)));
        }

        return productsQuery
            .Select(p => p.Maker)
            .Distinct()
            .ToArray();
    }

    private IReadOnlyCollection<MakerModel> MapMakers(
        IReadOnlyCollection<int> filterMakers,
        IReadOnlyCollection<Maker> allMakers,
        IReadOnlyCollection<Maker> filteredMakers)
    {
        var resultMakers = allMakers
            .Select(x => new MakerModel
            {
                Id = x.MakerId,
                Name = x.Name,
            })
            .ToArray();

        foreach(var maker in resultMakers)
        {
            maker.IsSelected = filterMakers.Contains(maker.Id);
            maker.IsEnabled = maker.IsSelected ? true : filteredMakers.Any(m => m.MakerId == maker.Id);
        }

        return resultMakers;
    }

    private record ParamValue(int ParamId, string ParamName, string Value);

    private async Task<IReadOnlyCollection<ParamValue>> GetParamValues(int productTypeId,
        IReadOnlyCollection<int> makers,
        IReadOnlyCollection<ProductFilterParam> paramValues)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId);

        if (makers.Any())
        {
            productsQuery = productsQuery.Where(x => makers.Contains(x.MakerId));
        }

        foreach (var filter in paramValues)
        {
            productsQuery = productsQuery
                .Where(p => p.ParamValues.Any(pv => filter.ParamId == pv.ParamId && filter.Values.Contains(pv.Value)));
        }

        //Получаем уникальные параметры товаров
        var results = await productsQuery
            .Include(x => x.ParamValues).ThenInclude(pv => pv.Param)
            .SelectMany(x => x.ParamValues)
            .Select(x => new
            {
                x.ParamId,
                x.Param.Name,
                x.Value,
            })
            .Distinct()
            .Select(x => new ParamValue(x.ParamId, x.Name, x.Value))
            .OrderBy(x => x.ParamName).ThenBy(x => x.Value)
            .ToArrayAsync();

        return results;
    }

    private IReadOnlyCollection<ProductParamModel> MapParamValues(
        IReadOnlyCollection<ParamValue> allParamValues,
        IReadOnlyCollection<ParamValue> filteredParamValues,
        IReadOnlyCollection<ProductFilterParam> filters)
    {
        var results = allParamValues
            .GroupBy(x => new
            {
                x.ParamId,
                x.ParamName,
            })
            .Select(group => new ProductParamModel
            {
                ParamId = group.Key.ParamId,
                ParamName = group.Key.ParamName,

                Values = group
                .Select(x => new ValueModel
                {
                    Value = x.Value,
                }).ToArray()
            }).ToArray();

        if (filters != null && filters.Any())
        {
            //Помечаем выбранные и недоступные фильтры
            foreach (var param in results)
            {
                foreach (var value in param.Values)
                {
                    bool isSelected = filters.Any(f => f.ParamId == param.ParamId && f.Values.Any(fv => fv == value.Value));
                    bool IsEnabled = filteredParamValues.Any(fpv => fpv.ParamId == param.ParamId && fpv.Value == value.Value);

                    value.IsSelected = isSelected;
                    value.IsEnabled = isSelected ? true : IsEnabled;
                }
            }
        }

        return results;
    }


    public async Task<Result<ProductListModel>> GetProductTypeProducts(ProductParams param)
    {
        var productType = await GetProductType(param.ProductTypeId);
        var products = await GetProducts(param);

        var allMakers = await GetMakers(param.ProductTypeId, Array.Empty<int>(), Array.Empty<ProductFilterParam>());
        var filteredMakers = await GetMakers(param.ProductTypeId, param.Makers, param.Params);

        var allParamValues = await GetParamValues(param.ProductTypeId, Array.Empty<int>(), Array.Empty<ProductFilterParam>());
        var filteredParamValues = await GetParamValues(param.ProductTypeId, param.Makers, param.Params);

        var model = new ProductListModel();
        model.CurrentCount = products.Count;

        ///Формируем выходную модель товаров
        model.Products = MapProducts(products);
        //Список фильтров - производитель
        model.Filters.Makers = MapMakers(param.Makers, allMakers, filteredMakers);
        ///Выходной набор фильтров
        model.Filters.Params = MapParamValues(allParamValues, filteredParamValues, param.Params);

        return model;
    }

}
