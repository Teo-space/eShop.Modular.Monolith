using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models.Params;

/// <summary>
/// Группа параметров товаров
/// </summary>
public class ProductTypeParamGroup : IDeletable
{
    public int ParamGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int Order { get; set; }

    public List<ProductTypeParam> Params { get; private set; } = new List<ProductTypeParam>();

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductTypeParamGroup() { }

    public static ProductTypeParamGroup Create(string name, string description)
    {
        return new ProductTypeParamGroup
        {
            Name = name,
            Description = description,
            Order = int.MaxValue,
        };
    }

    public static ProductTypeParamGroup CreateExists(int paramGroupId, string name, string description)
    {
        return new ProductTypeParamGroup
        {
            ParamGroupId = paramGroupId,
            Name = name,
            Description = description,
            Order = int.MaxValue,
        };
    }

}
