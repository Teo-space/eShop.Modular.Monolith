using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public sealed class ProductGroup : IDeletable
{
    public int ProductGroupId { get; set; }

    public int ParentProductGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductGroup() { }

    public static ProductGroup Create(ProductGroup parentProductGroup, string name, string description)
    {
        return new ProductGroup
        {
            ParentProductGroupId = parentProductGroup?.ProductGroupId ?? 0,
            Name = name,
            Description = description,
            IsDeleted = false,
        };
    }

    public static ProductGroup CreateExists(int productGroupId, ProductGroup parentProductGroup, string name, string description)
    {
        return new ProductGroup
        {
            ProductGroupId = productGroupId,
            ParentProductGroupId = parentProductGroup?.ProductGroupId ?? 0,
            Name = name,
            Description = description,
            IsDeleted = false,
        };
    }
}
