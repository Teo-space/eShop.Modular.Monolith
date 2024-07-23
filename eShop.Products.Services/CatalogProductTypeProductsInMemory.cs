using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Params.CatalogService;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Services;

internal class CatalogProductTypeProductsInMemory(ICatalogDbContext catalogDbContext) : ICatalogProductTypeProducts
{
    private async Task<ProductType> GetProductType(Guid productTypeId)
    {
        return await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync()
            ?? throw new Exception($"Not Found By Id {productTypeId}");
    }

    private async Task<IReadOnlyCollection<Product>> GetProducts(Guid productTypeId)
    {
        ///Выгружаем все товары по типу productTypeId
        var products = await catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .Include(x => x.Maker)
            .Include(x => x.ParamValues).ThenInclude(x => x.Param)
            .OrderBy(x => x.Name)
            .ToArrayAsync();

        return products;
    }

    private IReadOnlyCollection<Product> FilterProducts(
        IReadOnlyCollection<Product> products,
        IReadOnlyCollection<ProductFilterParam> filters)
    {
        if (filters.Any())
        {
            products = products
                .Where(p => filters
                .All(f => p.ParamValues
                .Any(pv => f.ParamId == pv.ParamId && f.Values.Contains(pv.Value))))
                .ToArray();
        }

        return products;
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
        }).ToArray();
    }

    private record ParamValue(int ParamId, string ParamName, string Value);

    private IReadOnlyCollection<ParamValue> GetParamValues(
        IReadOnlyCollection<Product> products, 
        IReadOnlyCollection<ProductFilterParam> filters)
    {
        if(filters.Any())
        {
            products = products
                .Where(p => filters
                .All(f => p.ParamValues
                .Any(pv => f.ParamId == pv.ParamId && f.Values.Contains(pv.Value))))
                .ToArray();
        }

        //Получаем уникальные параметры товаров
        var paramValues = products
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
            .ToArray();

        return paramValues;
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

    /// <summary>
    /// Метод получения списка товаров с фильтрацией
    /// В памяти
    /// </summary>
    public async Task<ProductListModel> GetProductTypeProducts(Guid productTypeId, IReadOnlyCollection<ProductFilterParam> filters)
    {
        var productType = await GetProductType(productTypeId);
        var allProducts = await GetProducts(productTypeId);
        var filteredProducts = FilterProducts(allProducts, filters);

        //Получаем уникальные параметры товаров
        var allParamValues = GetParamValues(allProducts, Array.Empty<ProductFilterParam>());
        var filteredParamValues = GetParamValues(allProducts, filters);

        var model = new ProductListModel();

        ///Формируем выходную модель товаров
        model.Products = MapProducts(filteredProducts);
        model.Params = MapParamValues(allParamValues, filteredParamValues, filters);

        return model;
    }

}
