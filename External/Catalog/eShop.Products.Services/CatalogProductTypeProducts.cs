using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Enum;
using eShop.Products.Interfaces.Params.Catalog;
using eShop.Products.Interfaces.Repositories;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog.ProductList;

namespace eShop.Products.Services;

/// <summary>
/// Каталог - выдача товаров с фильтрацией
/// </summary>
internal class CatalogProductTypeProducts(ICatalogRepository catalogRepository) : ICatalogProductTypeProducts
{
    public async Task<Result<ProductListModel>> GetProductTypeProducts(ProductParams param)
    {
        var productTypeResult = await catalogRepository.GetProductType(param.ProductTypeId);
        if (!productTypeResult.Success)
        {
            return productTypeResult.MapTo<ProductType, ProductListModel>();
        }

        var allDomainProducts = await catalogRepository.GetProducts(param.ProductTypeId);

        var allProducts = MapProducts(param, allDomainProducts);
        var allMakers = GetMakers(allProducts);
        var allParamValues = GetParamValues(allProducts);

        var filteredProducts = FilterProducts(param, allProducts);
        var preparedFilters = PrepareFilters(param, filteredProducts, allMakers, allParamValues);
        var sortedPaginatedProducts = SortAndPaginateProducts(param, filteredProducts);

        return new ProductListModel
        {
            TotalCount = filteredProducts.Count,
            CurrentCount = sortedPaginatedProducts.Count,
            Products = sortedPaginatedProducts,

            PageNumber = param.PageNumber,
            PageSize = param.PageSize,

            Filters = new ProductFiltersModel
            {
                MakersCount = preparedFilters.Makers.Count,
                Makers = preparedFilters.Makers,

                ParamsCount = preparedFilters.ParamValues.Count,
                Params = preparedFilters.ParamValues,
            }
        };
    }

    private IReadOnlyCollection<ProductModel> FilterProducts(ProductParams param, IReadOnlyCollection<ProductModel> products)
    {
        var productsQuery = products.AsEnumerable();
        {
            if (param.Makers.HasAny())
            {
                productsQuery = productsQuery.Where(x => param.Makers.Contains(x.MakerId));
            }

            foreach (var filter in param.Params)
            {
                productsQuery = productsQuery.Where(p => p.ParamGroups
                    .Any(pg => pg.Params.Any(pv => filter.ParamId == pv.ParamId && filter.Values.Contains(pv.Value))));
            }

            if (param.HasStars)
            {
                productsQuery = productsQuery.Where(p => p.Stars > 0);
            }
            if (param.HasReviews)
            {
                productsQuery = productsQuery.Where(p => p.ReviewsCount > 0);
            }
        }

        return productsQuery.ToArray();
    }

    private IReadOnlyCollection<ProductModel> MapProducts(ProductParams param, IReadOnlyCollection<Product> products)
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

            Price = param.RegionId.HasValue
                    ? x.Warehouses.FirstOrDefault(w => w.RegionId == param.RegionId)?.Price ?? 0
                    : x.Warehouses.Any() ? x.Warehouses.Max(w => w.Price) : 0,

            Availability = param.RegionId.HasValue
                    ? x.Warehouses.FirstOrDefault(w => w.RegionId == param.RegionId)?.Quantity ?? 0
                    : x.Warehouses.Sum(w => w.Quantity),

            SalesCount = x.Filters.SalesCount,
            Stars = x.Filters.Stars,
            StarsCount = x.Filters.StarsCount,
            ReviewsCount = x.Filters.ReviewsCount,

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
                Params = group
                .Select(paramValue => new ProductParamValueModel
                {
                    ParamId = paramValue.ParamId,
                    ParamName = paramValue.Param.Name,
                    Value = paramValue.Value,
                })
                .OrderBy(x => x.ParamName).ThenBy(x => x.Value)
                .ToArray()
            }).ToArray()
        })
        .OrderBy(x => x.MakerName).ThenBy(x => x.Name)
        .ToArray();
    }

    private IReadOnlyCollection<ProductModel> SortAndPaginateProducts(ProductParams param, IReadOnlyCollection<ProductModel> products)
    {
        var productsQuery = products.AsEnumerable();

        if (param.ProductsSorting == ProductsSorting.PriceAscending)
        {
            productsQuery = productsQuery.OrderBy(x => x.Price).ThenBy(x => x.MakerName).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == ProductsSorting.PriceDescending)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.Price).ThenBy(x => x.MakerName).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == ProductsSorting.MostPopular)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.SalesCount).ThenBy(x => x.MakerName).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == ProductsSorting.MostReviewed)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.ReviewsCount).ThenBy(x => x.MakerName).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == ProductsSorting.MostStar)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.Stars).ThenBy(x => x.MakerName).ThenBy(x => x.Name);
        }
        else
        {
            productsQuery = productsQuery.OrderBy(x => x.MakerName).ThenBy(x => x.Name);
        }

        return productsQuery
            .Skip(((param.PageNumber >= param.PageNumber ? param.PageNumber : 1) - 1) * param.PageSize)
            .Take(param.PageSize)
            .ToArray();
    }

    private IReadOnlyCollection<MakerFilterModel> GetMakers(IReadOnlyCollection<ProductModel> products)
    {
        var resultMakers = products
            .Select(m => new
            {
                m.MakerId,
                m.MakerName,
            })
            .Distinct()
            .Select(x => new MakerFilterModel
            {
                Id = x.MakerId,
                Name = x.MakerName,
            })
            .ToArray();

        return resultMakers;
    }

    private IReadOnlyCollection<ProductParamFilterModel> GetParamValues(IReadOnlyCollection<ProductModel> products)
    {
        var results = products
            .SelectMany(p => p.ParamGroups)
            .SelectMany(pg => pg.Params)
            .Select(pv => new
            {
                pv.ParamId,
                pv.ParamName,
                pv.Value
            })
            .Distinct()
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


        return results;
    }


    private record PreparedFilters(IReadOnlyCollection<MakerFilterModel> Makers, IReadOnlyCollection<ProductParamFilterModel> ParamValues);
    private PreparedFilters PrepareFilters(ProductParams param,
        IReadOnlyCollection<ProductModel> filteredProducts,
        IReadOnlyCollection<MakerFilterModel> allMakers,
        IReadOnlyCollection<ProductParamFilterModel> allParamValues)
    {
        if (param.Makers.HasNoOne() && param.Params.HasNoOne() && param.HasStars == false && param.HasReviews == false)
        {
            foreach (var makerFilter in allMakers)
            {
                makerFilter.IsSelected = false;
                makerFilter.IsEnabled = true;

                makerFilter.Count = filteredProducts.Count(p => p.MakerId == makerFilter.Id);
            }
            foreach (var paramValueFilter in allParamValues)
            {
                foreach (var valueFilter in paramValueFilter.Values)
                {
                    valueFilter.IsSelected = false;
                    valueFilter.IsEnabled = true;

                    valueFilter.Count = filteredProducts
                        .Count(p => p.ParamGroups
                        .Any(pg => pg.Params
                        .Any(pv => pv.ParamId == paramValueFilter.ParamId && pv.Value == valueFilter.Value)));
                }
            }
        }
        else
        {
            var filteredMakers = filteredProducts.Select(p => p.MakerId).Distinct().ToArray();
            foreach (var makerFilter in allMakers)
            {
                makerFilter.IsSelected = param.Makers.Contains(makerFilter.Id);

                makerFilter.Count = filteredProducts.Count(p => p.MakerId == makerFilter.Id);

                if (param.Makers.HasAny() && param.Params.HasNoOne() && param.HasStars == false && param.HasReviews == false)
                {
                    makerFilter.IsEnabled = true;
                }
                else
                {
                    makerFilter.IsEnabled = filteredMakers.Contains(makerFilter.Id);
                }
            }

            var filteredParamValues = GetParamValues(filteredProducts);
            foreach (var paramValueFilter in allParamValues)
            {
                var inputFilterParam = param.Params.FirstOrDefault(p => p.ParamId == paramValueFilter.ParamId);
                paramValueFilter.IsSelected = inputFilterParam != null;

                foreach (var valueFilter in paramValueFilter.Values)
                {
                    if (inputFilterParam != null)
                    {
                        valueFilter.IsSelected = inputFilterParam.Values.Contains(valueFilter.Value);
                    }

                    valueFilter.Count = filteredProducts
                        .Count(p => p.ParamGroups
                        .Any(pg => pg.Params
                        .Any(pv => pv.ParamId == paramValueFilter.ParamId && pv.Value == valueFilter.Value)));

                    if (paramValueFilter.IsSelected &&
                        param.Makers.HasNoOne() && param.Params.HasOne() && param.HasStars == false && param.HasReviews == false)
                    {
                        valueFilter.IsEnabled = true;
                    }
                    else
                    {
                        valueFilter.IsEnabled = filteredParamValues
                            .Any(fp => fp.ParamId == paramValueFilter.ParamId && fp.Values.Any(v => v.Value == valueFilter.Value));
                    }
                }
            }
        }

        allMakers = allMakers.OrderByDescending(m => m.IsSelected).ThenByDescending(m => m.IsEnabled).ThenBy(m => m.Name).ToArray();
        allParamValues = allParamValues
                .OrderByDescending(m => m.IsSelected).ThenBy(m => m.ParamName)
                .Select(p =>
                {
                    p.Values = p.Values.OrderByDescending(m => m.IsSelected).ThenByDescending(m => m.IsEnabled).ThenBy(m => m.Value).ToArray();
                    return p;
                })
                .ToArray();

        return new PreparedFilters(allMakers, allParamValues);
    }
}
