using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Params.CatalogService;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Services;

internal class CatalogService(ICatalogDbContext catalogDbContext) : ICatalogService
{
    public async Task<IReadOnlyCollection<ProductGroup>> GetProductGroups()
        => (await catalogDbContext.ProductGroups.AsNoTracking().ToArrayAsync());

    public async Task<ProductGroupTreeModel> GetProductGroupsTree()
    {
        var model = new ProductGroupTreeModel();

        var productGroups = await GetProductGroups();
        model.TotalCount = productGroups.Count;

        model.ProductGroups = productGroups
            .Where(x => x.ParentProductGroupId == Guid.Empty)
            .Select(productGroup => new ProductGroupModel()
            {
                ProductGroupId = productGroup.ProductGroupId,
                Name = productGroup.Name,
                Description = productGroup.Description,
            })
            .OrderBy(x => x.Name)
            .ToArray();

        List<ProductGroupModel> levelResults = new List<ProductGroupModel>(model.ProductGroups);
        List<ProductGroupModel> nextLevelResults = new List<ProductGroupModel>();

        while (levelResults.Any())
        {
            foreach (var productGroup in levelResults)
            {
                var childs = productGroups
                    .Where(x => x.ParentProductGroupId == productGroup.ProductGroupId)
                    .Select(productGroup => new ProductGroupModel()
                    {
                        ProductGroupId = productGroup.ProductGroupId,
                        Name = productGroup.Name,
                        Description = productGroup.Description,
                    })
                    .OrderBy(x => x.Name)
                    .ToArray();

                if (childs.Any())
                {
                    productGroup.Childs = childs;
                    nextLevelResults.AddRange(childs);
                }
            }

            levelResults.Clear();
            if (nextLevelResults.Any())
            {
                levelResults.AddRange(nextLevelResults);
                nextLevelResults.Clear();
            }
        }

        return model;
    }

    public async Task<ProductGroupTypesModel> GetProductGroupTypes(Guid productGroupId)
    {
        var productGroup = await catalogDbContext.ProductGroups
            .AsNoTracking()
            .Where(x => x.ProductGroupId == productGroupId)
            .FirstOrDefaultAsync()
            ?? throw new Exception($"Not Found By Id {productGroupId}");

        var productTypes = await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductGroupId == productGroupId)
            .OrderBy(x => x.Name)
            .Select(x => new ProductTypeModel
            {
                ProductTypeId = x.ProductTypeId,
                Name = x.Name,
                Description = x.Description,
            }).ToArrayAsync();

        var model = new ProductGroupTypesModel
        {
            ProductGroupId = productGroup.ProductGroupId,
            Name = productGroup.Name,
            Description = productGroup.Description,

            Types = productTypes
        };

        return model;
    }

    public async Task<ProductListModel> GetProductTypeProducts(Guid productTypeId, IReadOnlyCollection<ProductFilterParam> filters)
    {
        var productType = await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync()
            ?? throw new Exception($"Not Found By Id {productTypeId}");

        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId);

        if (filters.Any())
        {
            productsQuery = productsQuery
                .Where(p => filters.All(f => p.ParamValues.Any(pv => f.ParamId == pv.ParamId && f.Values.Contains(pv.Value))));
        }

        var products = await productsQuery.OrderBy(x => x.Name).ToArrayAsync();

        var allParamValues = await catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .Include(x => x.ParamValues)
            .SelectMany(x => x.ParamValues)
            .Select(x => new
            {
                x.ParamId,
                x.Param.Name,
                x.Value,
            })
            .Distinct()
            .OrderBy(x => x.ParamId)
            .ToArrayAsync();

        var model = new ProductListModel();

        ///Выходной набор фильтров
        model.Params = allParamValues
            .GroupBy(x => new
            {
                x.ParamId,
                x.Name,
            })
            .Select(group => new ProductParamModel
            {
                ParamId = group.Key.ParamId,
                Name = group.Key.Name,
                Values = group.Select(x => new ValueModel
                {
                    Value = x.Value,
                }).ToArray()
            }).ToArray();

        if (filters.Any())
        {
            //Получаем список параметров товаров после фильтрации
            var filteredParamValues = await catalogDbContext.Products
                .AsNoTracking()
                .Where(x => x.ProductTypeId == productTypeId)
                .Where(p => filters.All(f => p.ParamValues.Any(pv => f.ParamId == pv.ParamId && f.Values.Contains(pv.Value))))
                .Include(x => x.ParamValues)
                .SelectMany(x => x.ParamValues)
                .Select(x => new
                {
                    x.ParamId,
                    x.Param.Name,
                    x.Value,
                })
                .Distinct()
                .OrderBy(x => x.Name)
                .ToArrayAsync();

            //Помечаем выбранные и недоступные фильтры
            foreach (var param in model.Params)
            {
                foreach (var value in param.Values)
                {
                    bool isSelected = filters.Any(f => f.ParamId == param.ParamId && f.Values.Any(fv => fv == value.Value));
                    bool IsDisabled = !filteredParamValues.Any(fpv => fpv.ParamId == param.ParamId && fpv.Value == value.Value);

                    value.IsSelected = isSelected;
                    value.IsDisabled = isSelected ? false : IsDisabled;
                }
            }
        }

        ///Формируем выходную модель товаров
        model.Products = products.Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            MakerId = x.MakerId,
            MakerName = x.Maker.Name,
            Number = x.Number,
            Name = x.Name,
            Description = x.Description,
        }).ToArray();

        return model;
    }

    /// <summary>
    /// Метод получения списка товаров с фильтрацией
    /// В памяти
    /// </summary>
    /// <param name="productTypeId"></param>
    /// <param name="filters"></param>
    public async Task<ProductListModel> GetProductTypeProductsInMem(Guid productTypeId, IReadOnlyCollection<ProductFilterParam> filters)
    {
        var productType = await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync()
            ?? throw new Exception($"Not Found By Id {productTypeId}");

        ///Выгружаем все товары по типу productTypeId
        var products = await catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .Include(x => x.Maker)
            .Include(x => x.ParamValues).ThenInclude(x => x.Param)
            .OrderBy(x => x.Name)
            .ToArrayAsync();

        //Получаем уникальные параметры товаров
        var allParamValues = products
            .SelectMany(x => x.ParamValues)
            .Select(x => new
            {
                x.ParamId,
                x.Param.Name,
                x.Value,
            })
            .Distinct()
            .OrderBy(x => x.Name).ThenBy(x => x.Value)
            .ToArray();

        var model = new ProductListModel();

        ///Выходной набор фильтров
        model.Params = allParamValues
            .GroupBy(x => new
            {
                x.ParamId,
                x.Name,
            })
            .Select(group => new ProductParamModel
            {
                ParamId = group.Key.ParamId,
                Name = group.Key.Name,
                Values = group.Select(x => new ValueModel
                {
                    Value = x.Value,
                }).ToArray()
            }).ToArray();

        if (filters != null && filters.Any())
        {
            //Получаем список параметров товаров после фильтрации
            var filteredParamValues = products
                .Where(product => product.ParamValues
                .Any(paramValue => filters
                .Any(f => f.ParamId == paramValue.ParamId && f.Values.Contains(paramValue.Value))))
                .SelectMany(x => x.ParamValues)
                .Select(x => new
                {
                    x.ParamId,
                    x.Param.Name,
                    x.Value,
                })
                .Distinct()
                .OrderBy(x => x.ParamId)
                .ToArray();
            //Фильтруем товары
            products = products
                .Where(product => product.ParamValues
                .Any(paramValue => filters
                .Any(f => f.ParamId == paramValue.ParamId && f.Values.Contains(paramValue.Value))))
                .ToArray();

            //Помечаем выбранные и недоступные фильтры
            foreach(var param in model.Params)
            {
                foreach(var value in param.Values)
                {
                    bool isSelected = filters.Any(f => f.ParamId == param.ParamId && f.Values.Any(fv => fv == value.Value));
                    bool IsDisabled = !filteredParamValues.Any(fpv => fpv.ParamId == param.ParamId && fpv.Value == value.Value);

                    value.IsSelected = isSelected;
                    value.IsDisabled = isSelected ? false : IsDisabled;
                }
            }
        }
        ///Формируем выходную модель товаров
        model.Products = products.Select(x => new ProductModel
        {
            ProductId = x.ProductId,
            MakerId = x.MakerId,
            MakerName = x.Maker.Name,
            Number = x.Number,
            Name = x.Name,
            Description = x.Description,
        }).ToArray();

        return model;
    }
}

