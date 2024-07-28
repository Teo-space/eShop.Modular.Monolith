using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductTypeParam : IDeletable
{
    public int ProductTypeId { get; set; }
    public int ParamId { get; set; }

    public bool IsDeleted { get; set; }
}
