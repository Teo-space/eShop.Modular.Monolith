namespace eShop.Products.Models.Catalog;

public class ProductModel
{
    public Guid ProductId { get; set; }

    public Guid MakerId { get; set; }
    public string MakerName { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
