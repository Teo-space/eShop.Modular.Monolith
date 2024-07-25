namespace eShop.Products.Models.Catalog.ProductList;

public sealed class ProductListModel
{
    public int TotalCount { get; set; }
    public int CurrentCount { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public IReadOnlyCollection<ProductModel> Products { get; set; } = Array.Empty<ProductModel>();

    public ProductFiltersModel Filters { get; set; } = new ProductFiltersModel();
}


