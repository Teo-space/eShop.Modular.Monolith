using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductTypeParam : IDeletable
{
    public Guid ProductTypeId { get; set; }
    public int ParamId { get; set; }

    public bool IsDeleted { get; set; }
}
