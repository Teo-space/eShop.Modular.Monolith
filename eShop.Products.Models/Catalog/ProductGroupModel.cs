namespace eShop.Products.Models.Catalog;

public class ProductGroupModel
{
    public Guid ProductGroupId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<ProductGroupModel> Childs { get; set; } = Array.Empty<ProductGroupModel>();
}
