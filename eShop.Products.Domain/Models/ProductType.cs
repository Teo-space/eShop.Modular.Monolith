using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductType : IDeletable
{
    public Guid ProductTypeId { get; set; }

    public Guid ProductGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
