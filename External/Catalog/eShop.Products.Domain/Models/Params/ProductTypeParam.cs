using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models.Params;

public class ProductTypeParam : IDeletable
{
    public int ParamId { get; private set; }

    public int ProductTypeId { get; private set; }
    public ProductType ProductType { get; private set; }

    public int ParamGroupId { get; private set; }
    public ProductTypeParamGroup ParamGroup { get; private set; }

    public bool IsDeleted { get; set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public int Order { get; set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductTypeParam() { }

    public static ProductTypeParam Create(ProductType productType, ProductTypeParamGroup paramGroup, string name, string description)
    {
        return new ProductTypeParam
        {
            ProductTypeId = productType.ProductTypeId,
            ParamGroupId = paramGroup.ParamGroupId,
            IsDeleted = false,
            Name = name,
            Description = description
        };
    }

    public static ProductTypeParam CreateExists(int paramId,
        ProductType productType, ProductTypeParamGroup paramGroup, string name, string description)
    {
        return new ProductTypeParam
        {
            ParamId = paramId,
            ProductTypeId = productType.ProductTypeId,
            ParamGroupId = paramGroup.ParamGroupId,
            IsDeleted = false,
            Name = name,
            Description = description
        };
    }
}
