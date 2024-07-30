using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Models;
using eShop.Products.Interfaces.Params.Catalog;

namespace eShop.Products.Interfaces.Repositories;

public interface ICatalogRepository
{
    Task<ProductGroup> GetProductGroup(int productGroupId);

    Task<IReadOnlyCollection<ProductGroup>> GetProductGroups();

    Task<ProductType> GetProductType(int productTypeId);

    Task<IReadOnlyCollection<ProductType>> GetProductTypes(int productGroupId);


    Task<IReadOnlyCollection<Product>> GetProducts(ProductParams param);

    Task<int> GetProductsTotalCount(ProductParams param);

    Task<IReadOnlyCollection<Maker>> GetMakersFilters(ProductParams param);

    Task<IReadOnlyCollection<ParamValueFilterModel>> GetParamValuesFilters(ProductParams param);
}
