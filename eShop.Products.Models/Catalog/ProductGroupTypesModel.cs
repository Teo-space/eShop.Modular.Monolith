namespace eShop.Products.Models.Catalog;

public class ProductGroupTypesModel
{
    public Guid ProductGroupId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<ProductTypeModel> Types { get; set; } = Array.Empty<ProductTypeModel>();
}


