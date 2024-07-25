namespace eShop.Products.Models.Catalog.ProductGroups;

public class ProductGroupTreeModel
{
    public IReadOnlyCollection<ProductGroupModel> ProductGroups { get; set; } = Array.Empty<ProductGroupModel>();

    public int TotalCount { get; set; } = 0;
}
