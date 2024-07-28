namespace eShop.Products.Models.Catalog.ProductList;

public sealed class ProductFiltersModel
{
    public IReadOnlyCollection<MakerFilterModel> Makers { get; set; } = Array.Empty<MakerFilterModel>();
    public IReadOnlyCollection<ProductParamFilterModel> Params { get; set; } = Array.Empty<ProductParamFilterModel>();
}

public sealed class MakerFilterModel
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsSelected { get; set; }

    public bool IsEnabled { get; set; }
}

public sealed class ProductParamFilterModel
{
    public int ParamId { get; set; }
    public string ParamName { get; set; }

    public IReadOnlyCollection<ValueFilterModel> Values { get; set; } = Array.Empty<ValueFilterModel>();
}

public sealed class ValueFilterModel
{
    public string Value { get; set; }

    public bool IsSelected { get; set; }

    public bool IsEnabled { get; set; }
}
