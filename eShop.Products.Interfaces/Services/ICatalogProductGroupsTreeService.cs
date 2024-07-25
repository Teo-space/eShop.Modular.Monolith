using eShop.Products.Domain.Models;
using eShop.Products.Models.Catalog.ProductGroups;

namespace eShop.Products.Interfaces.Services;

public interface ICatalogProductGroupsTreeService
{
    /// <summary>
    /// Получение списка товарных подгрупп
    /// </summary>
    Task<IReadOnlyCollection<ProductGroup>> GetProductGroups();

    /// <summary>
    /// Получение дерева товарных подгрупп
    /// </summary>
    Task<Result<ProductGroupTreeModel>> GetProductGroupsTree();
}
