using eShop.Products.Models.Catalog.ProductGroupTypes;

namespace eShop.Products.Interfaces.Services;

public interface ICatalogProductGroupTypes
{
    /// <summary>
    /// Получение списка типов товаров в товарной подгруппе
    /// </summary>
    Task<Result<ProductGroupTypesModel>> GetProductGroupTypes(Guid productGroupId);
}
