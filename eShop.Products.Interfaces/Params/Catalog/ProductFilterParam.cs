namespace eShop.Products.Interfaces.Params.Catalog;

public class ProductFilterParam
{
    public int ParamId { get; set; }

    public IReadOnlyCollection<string> Values { get; set; } = Array.Empty<string>();
}
