using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Models;
using eShop.Products.Interfaces.Params.Catalog;
using eShop.Products.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Repositories;

internal class CatalogRepository(ICatalogDbContext catalogDbContext) : ICatalogRepository
{
    public async Task<ProductGroup> GetProductGroup(int productGroupId)
    {
        return await catalogDbContext.ProductGroups
            .AsNoTracking()
            .Where(x => x.ProductGroupId == productGroupId)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundApiException($"Not Found By Id {productGroupId}");
    }

    public async Task<IReadOnlyCollection<ProductGroup>> GetProductGroups()
        => (await catalogDbContext.ProductGroups.AsNoTracking().ToArrayAsync());

    public async Task<IReadOnlyCollection<ProductType>> GetProductTypes(int productGroupId)
    {
        return await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductGroupId == productGroupId)
            .OrderBy(x => x.Name)
            .ToArrayAsync();
    }

    public async Task<ProductType> GetProductType(int productTypeId)
    {
        return await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync()
            ?? throw new KeyNotFoundApiException($"Not Found By Id {productTypeId}");
    }

    private IQueryable<Product> ApplyFilters(IQueryable<Product> productsQuery, ProductParams param)
    {
        if (param.Makers.NotEmpty())
        {
            productsQuery = productsQuery.Where(x => param.Makers.Contains(x.MakerId));
        }

        foreach (var filter in param.Params)
        {
            productsQuery = productsQuery
                .Where(p => p.ParamValues.Any(pv => filter.ParamId == pv.ParamId && filter.Values.Contains(pv.Value)));
        }

        if (param.HasStars)
        {
            productsQuery = productsQuery.Where(p => p.Stars > 0);
        }
        if (param.HasReviews)
        {
            productsQuery = productsQuery.Where(p => p.ReviewsCount > 0);
        }

        return productsQuery;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(ProductParams param)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == param.ProductTypeId)
            .Include(x => x.Maker)
            .Include(x => x.ParamValues).ThenInclude(x => x.Param).ThenInclude(x => x.ParamGroup)
            .AsQueryable();

        productsQuery = ApplyFilters(productsQuery, param);

        if (param.ProductsSorting == Interfaces.Enum.ProductsSorting.PriceAscending)
        {
            productsQuery = productsQuery.OrderBy(x => x.Price).ThenBy(x => x.Maker.Name).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == Interfaces.Enum.ProductsSorting.MostPopular)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.Price).ThenBy(x => x.Maker.Name).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == Interfaces.Enum.ProductsSorting.PriceDescending)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.SalesCount).ThenBy(x => x.Maker.Name).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == Interfaces.Enum.ProductsSorting.MostReviewed)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.ReviewsCount).ThenBy(x => x.Maker.Name).ThenBy(x => x.Name);
        }
        else if (param.ProductsSorting == Interfaces.Enum.ProductsSorting.MostStar)
        {
            productsQuery = productsQuery.OrderByDescending(x => x.Stars).ThenBy(x => x.Maker.Name).ThenBy(x => x.Name);
        }

        return await productsQuery
            .Skip(((param.PageNumber >= 1 ? param.PageNumber : 1) - 1) * param.PageSize).Take(param.PageSize)
            .ToArrayAsync();
    }

    public async Task<int> GetProductsTotalCount(ProductParams param)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == param.ProductTypeId)
            .AsQueryable();

        productsQuery = ApplyFilters(productsQuery, param);

        return await productsQuery.CountAsync();
    }

    public async Task<IReadOnlyCollection<Maker>> GetMakersFilters(ProductParams param)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == param.ProductTypeId)
            .Include(x => x.Maker)
            .AsQueryable();

        productsQuery = ApplyFilters(productsQuery, param);

        return await productsQuery
            .Select(p => p.Maker)
            .Distinct()
            .ToArrayAsync();
    }

    public async Task<IReadOnlyCollection<ParamValueFilterModel>> GetParamValuesFilters(ProductParams param)
    {
        var productsQuery = catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == param.ProductTypeId)
            .Include(x => x.ParamValues).ThenInclude(pv => pv.Param)
            .AsQueryable();

        productsQuery = ApplyFilters(productsQuery, param);

        //Получаем уникальные параметры товаров
        var results = await productsQuery
            .SelectMany(x => x.ParamValues)
            .Select(x => new
            {
                ParamId = x.ParamId,
                ParamName = x.Param.Name,
                Value = x.Value,
            })
            .Distinct()
            .Select(x => new ParamValueFilterModel
            {
                ParamId = x.ParamId,
                ParamName = x.ParamName,
                Value = x.Value,
            })
            .OrderBy(x => x.ParamName).ThenBy(x => x.Value)
            .ToArrayAsync();

        return results;
    }

}
