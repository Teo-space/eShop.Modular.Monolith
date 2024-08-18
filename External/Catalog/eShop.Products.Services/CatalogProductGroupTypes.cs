using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Repositories;
using eShop.Products.Interfaces.Services;
using eShop.Products.Models.Catalog.ProductGroupTypes;

namespace eShop.Products.Services;

internal class CatalogProductGroupTypes(ICatalogRepository catalogRepository) : ICatalogProductGroupTypes
{
    public async Task<Result<ProductGroupTypesModel>> GetProductGroupTypes(int productGroupId)
    {
        var productGroupResult = await catalogRepository.GetProductGroup(productGroupId);
        if (!productGroupResult.Success)
        {
            return productGroupResult.MapTo<ProductGroup, ProductGroupTypesModel>();
        }

        var productTypes = await catalogRepository.GetProductTypes(productGroupId);

        var productGroup = productGroupResult.Value;
        var model = new ProductGroupTypesModel
        {
            ProductGroupId = productGroup.ProductGroupId,
            Name = productGroup.Name,
            Description = productGroup.Description,

            Types = productTypes
            .Select(x => new ProductTypeModel
            {
                ProductTypeId = x.ProductTypeId,
                Name = x.Name,
                Description = x.Description,
            }).ToArray()
        };

        return model;
    }
}
