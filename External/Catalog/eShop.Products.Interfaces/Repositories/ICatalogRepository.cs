using eShop.Products.Domain.Models;

namespace eShop.Products.Interfaces.Repositories;

public interface ICatalogRepository
{
    Task<Result<ProductGroup>> GetProductGroup(int productGroupId);

    Task<IReadOnlyCollection<ProductGroup>> GetProductGroups();

    Task<Result<ProductType>> GetProductType(int productTypeId);

    Task<IReadOnlyCollection<ProductType>> GetProductTypes(int productGroupId);

    Task<IReadOnlyCollection<Product>> GetProducts(int productTypeId);

}
