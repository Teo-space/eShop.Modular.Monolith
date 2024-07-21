using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Params.CatalogService;
using eShop.Products.Models.Catalog;

namespace eShop.Products.Interfaces.Services;

/// <summary>
/// Каталог товаров
/// </summary>
public interface ICatalogService
{
    /// <summary>
    /// Получение списка товарных подгрупп
    /// </summary>
    Task<IReadOnlyCollection<ProductGroup>> GetProductGroups();

    /// <summary>
    /// Получение дерева товарных подгрупп
    /// </summary>
    Task<ProductGroupTreeModel> GetProductGroupsTree();

    /// <summary>
    /// Получение списка типов товаров в товарной подгруппе
    /// </summary>
    Task<ProductGroupTypesModel> GetProductGroupTypes(Guid productGroupId);

    /// <summary>
    /// Получение списка товаров по типу
    /// </summary>
    Task<ProductListModel> GetProductTypeProducts(Guid productTypeId, IReadOnlyCollection<ProductFilterParam> filters);


}
