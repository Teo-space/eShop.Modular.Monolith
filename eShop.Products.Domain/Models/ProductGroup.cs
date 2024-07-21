using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductGroup : IDeletable
{
    public Guid ProductGroupId { get; set; }

    public Guid ParentProductGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
