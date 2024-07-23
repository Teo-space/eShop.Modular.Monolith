using eShop.Products.Interfaces.Params.CatalogService;
using eShop.Products.Models.Catalog;

namespace eShop.Products.Interfaces.Services;

public interface ICatalogProductTypeProducts
{
    /// <summary>
    /// Получение списка товаров по типу
    /// </summary>
    Task<ProductListModel> GetProductTypeProducts(Guid productTypeId, IReadOnlyCollection<ProductFilterParam> filters);
}
