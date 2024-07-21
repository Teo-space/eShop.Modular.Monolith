namespace eShop.Products.Models.Catalog;

public class ProductListModel
{
    public IReadOnlyCollection<ProductModel> Products { get; set; } = Array.Empty<ProductModel>();
    public IReadOnlyCollection<ProductParamModel> Params { get; set; } = Array.Empty<ProductParamModel>();
}

public class ProductParamModel
{
    public int ParamId { get; set; }
    public string Name { get; set; }

    public IReadOnlyCollection<ValueModel> Values { get; set; } = Array.Empty<ValueModel>();
}

public class ValueModel
{
    public string Value { get; set; }

    public bool IsSelected { get; set; }

    public bool IsDisabled { get; set; }
}

