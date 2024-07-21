namespace eShop.Products.Interfaces.Params.CatalogService;

public class ProductFilterParam
{
    public int ParamId { get; set; }

    public IReadOnlyCollection<string> Values { get; set; } = Array.Empty<string>();
}
