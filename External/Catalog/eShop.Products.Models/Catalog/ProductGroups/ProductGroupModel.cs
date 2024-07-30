namespace eShop.Products.Models.Catalog.ProductGroups;

public class ProductGroupModel
{
    public int ProductGroupId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<ProductGroupModel> Childs { get; set; } = Array.Empty<ProductGroupModel>();
}
