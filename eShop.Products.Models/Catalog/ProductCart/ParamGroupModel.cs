namespace eShop.Products.Models.Catalog.ProductCart;

public class ParamGroupModel
{
    public int ParamGroupId { get; set; }

    public IReadOnlyCollection<ParamValue> ParamValues { get; set; } = Array.Empty<ParamValue>();
}

public class ParamValue
{
    public int ParamId { get; set; }
    public string Value { get; set; }
}