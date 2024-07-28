using eShop.Products.Interfaces.Params.Catalog;
using eShop.Products.Models.Catalog.ProductList;

namespace eShop.Products.Interfaces.Services;

public interface ICatalogProductTypeProducts
{
    /// <summary>
    /// Получение списка товаров по типу
    /// </summary>
    Task<Result<ProductListModel>> GetProductTypeProducts(ProductParams param);

}
