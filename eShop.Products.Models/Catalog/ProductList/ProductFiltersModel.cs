namespace eShop.Products.Models.Catalog.ProductList;

public sealed class ProductFiltersModel
{
    public IReadOnlyCollection<MakerModel> Makers { get; set; } = Array.Empty<MakerModel>();
    public IReadOnlyCollection<ProductParamModel> Params { get; set; } = Array.Empty<ProductParamModel>();
}

public sealed class MakerModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsSelected { get; set; }

    public bool IsEnabled { get; set; }
}

public sealed class ProductParamModel
{
    public int ParamId { get; set; }
    public string ParamName { get; set; }

    public IReadOnlyCollection<ValueModel> Values { get; set; } = Array.Empty<ValueModel>();
}

public sealed class ValueModel
{
    public string Value { get; set; }

    public bool IsSelected { get; set; }

    public bool IsEnabled { get; set; }
}
