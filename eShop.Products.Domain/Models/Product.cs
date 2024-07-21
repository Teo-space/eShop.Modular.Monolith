using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class Product : IDeletable
{
    public Guid ProductId { get; set; }
    public Guid ProductTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public Guid MakerId { get; set; }
    public Maker Maker { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public HashSet<ProductParamValue> ParamValues { get; set; }
}
