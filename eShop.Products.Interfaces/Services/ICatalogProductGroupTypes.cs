using eShop.Products.Models.Catalog;

namespace eShop.Products.Interfaces.Services;

public interface ICatalogProductGroupTypes
{
    /// <summary>
    /// Получение списка типов товаров в товарной подгруппе
    /// </summary>
    Task<ProductGroupTypesModel> GetProductGroupTypes(Guid productGroupId);
}
