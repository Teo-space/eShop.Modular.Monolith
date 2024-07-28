namespace eShop.Products.Models.Catalog.ProductGroupTypes;

public class ProductGroupTypesModel
{
    public int ProductGroupId { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public IReadOnlyCollection<ProductTypeModel> Types { get; set; } = Array.Empty<ProductTypeModel>();
}


