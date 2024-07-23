using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Services;

internal class CatalogProductGroupTypes(ICatalogDbContext catalogDbContext) : ICatalogProductGroupTypes
{
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

}
