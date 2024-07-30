using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductType : IDeletable
{
    public int ProductTypeId { get; set; }

    public int ProductGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
}
