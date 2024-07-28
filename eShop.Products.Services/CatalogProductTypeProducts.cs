using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Models;
using eShop.Products.Interfaces.Params.Catalog;
using eShop.Products.Interfaces.Repositories;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog.ProductList;

namespace eShop.Products.Services;

internal class CatalogProductTypeProducts(ICatalogRepository catalogRepository) : ICatalogProductTypeProducts
{
    public async Task<Result<ProductListModel>> GetProductTypeProducts(ProductParams param)
    {
        var productType = await catalogRepository.GetProductType(param.ProductTypeId);
        var products = await catalogRepository.GetProducts(param);

        var emptyParams = param with
        {
            Makers = Array.Empty<int>(),
            Params = Array.Empty<ProductFilterParam>(),
            HasStars = false,
            HasReviews = false,
        };

        var allMakers = await catalogRepository.GetMakersFilters(emptyParams);
        var filteredMakers = await catalogRepository.GetMakersFilters(param);

        var allParamValues = await catalogRepository.GetParamValuesFilters(emptyParams);
        var filteredParamValues = await catalogRepository.GetParamValuesFilters(param);

        var model = new ProductListModel();

        model.PageNumber = param.PageNumber;
        model.PageSize = param.PageSize;
        model.TotalCount = await catalogRepository.GetProductsTotalCount(param);
        model.CurrentCount = products.Count;

        ///Формируем выходную модель товаров
        model.Products = MapProducts(products);
        //Список фильтров - производитель
        model.Filters.Makers = MapMakers(param.Makers, allMakers, filteredMakers);
        ///Выходной набор фильтров
        model.Filters.Params = MapParamValues(allParamValues, filteredParamValues, param.Params);

        return model;
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
            Price = x.Price,
            Availability = x.Availability,
            SalesCount = x.SalesCount,
            Stars = x.Stars,
            StarsCount = x.StarsCount,
            ReviewsCount = x.ReviewsCount,

            ParamGroups = x.ParamValues
            .GroupBy(x => new
            {
                x.Param.ParamGroup.ParamGroupId,
                x.Param.ParamGroup.Name,
                x.Param.ParamGroup.Order,
            })
            .OrderBy(group => group.Key.Order).ThenBy(group => group.Key.Name)
            .Select(group => new ProductParamGroupModel
            {
                ParamGroupName = group.Key.Name,
                Params = group.Select(paramValue => new ProductParamValueModel
                {
                    ParamId = paramValue.ParamId,
                    ParamName = paramValue.Param.Name,
                    Value = paramValue.Value,
                }).ToArray()
            }).ToArray()
        })
        .OrderBy(x => x.MakerName).ThenBy(x => x.Name)
        .ToArray();
    }

    private IReadOnlyCollection<MakerFilterModel> MapMakers(
        IReadOnlyCollection<int> filterMakers,
        IReadOnlyCollection<Maker> allMakers,
        IReadOnlyCollection<Maker> filteredMakers)
    {
        var resultMakers = allMakers
            .Select(x => new MakerFilterModel
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

    private IReadOnlyCollection<ProductParamFilterModel> MapParamValues(
        IReadOnlyCollection<ParamValueFilterModel> allParamValues,
        IReadOnlyCollection<ParamValueFilterModel> filteredParamValues,
        IReadOnlyCollection<ProductFilterParam> filters)
    {
        var results = allParamValues
            .GroupBy(x => new
            {
                x.ParamId,
                x.ParamName,
            })
            .Select(group => new ProductParamFilterModel
            {
                ParamId = group.Key.ParamId,
                ParamName = group.Key.ParamName,

                Values = group
                .Select(x => new ValueFilterModel
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

}
