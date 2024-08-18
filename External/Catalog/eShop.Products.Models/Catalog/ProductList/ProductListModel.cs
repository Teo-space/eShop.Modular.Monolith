namespace eShop.Products.Models.Catalog.ProductList;

/// <summary>
/// Список товаров в категории
/// </summary>
public sealed class ProductListModel
{
    /// <summary>
    /// Количество товаров после фильтрации
    /// </summary>
    public int TotalCount { get; set; }
    /// <summary>
    /// Количество в текущем запросе с пагинацией
    /// </summary>
    public int CurrentCount { get; set; }

    /// <summary>
    /// Номер страницы
    /// </summary>
    public int PageNumber { get; set; }
    /// <summary>
    /// Количество товаров на странице
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Фильтры товаров
    /// </summary>
    public ProductFiltersModel Filters { get; set; } = new ProductFiltersModel();

    /// <summary>
    /// Товары
    /// </summary>
    public IReadOnlyCollection<ProductModel> Products { get; set; } = Array.Empty<ProductModel>();
}


