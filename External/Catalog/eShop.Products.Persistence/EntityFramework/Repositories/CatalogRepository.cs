using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Repositories;

internal class CatalogRepository(ICatalogDbContext catalogDbContext) : ICatalogRepository
{
    public async Task<Result<ProductGroup>> GetProductGroup(int productGroupId)
    {
        var prductGroup = await catalogDbContext.ProductGroups
            .AsNoTracking()
            .Where(x => x.ProductGroupId == productGroupId)
            .FirstOrDefaultAsync();
        if(prductGroup == null)
        {
            return Results.NotFound<ProductGroup>($"Товарная подгруппа {productGroupId} не найдена");
        }

        return prductGroup;
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

    public async Task<Result<ProductType>> GetProductType(int productTypeId)
    {
        var productType = await catalogDbContext.ProductTypes
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .FirstOrDefaultAsync();
        if (productType == null)
        {
            return Results.NotFound<ProductType>($"Тип товара {productTypeId} не найден");
        }

        return productType;
    }

    public async Task<IReadOnlyCollection<Product>> GetProducts(int productTypeId)
    {
        return await catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductTypeId == productTypeId)
            .Include(x => x.Maker)
            .Include(x => x.ParamValues).ThenInclude(x => x.Param).ThenInclude(x => x.ParamGroup)
            .ToArrayAsync();
    }

    public async Task<Result<Product>> GetProduct(int productd)
    {
        var product = await catalogDbContext.Products
            .AsNoTracking()
            .Where(x => x.ProductId == productd)
            .Where(x => x.IsDeleted == false)
            .Include(x => x.ProductType)
            .Include(x => x.Maker)
            .FirstOrDefaultAsync();

        if(product == null)
        {
            return Results.NotFound<Product>($"Товар '{productd}' не найден");
        }

        return product;
    }

}
