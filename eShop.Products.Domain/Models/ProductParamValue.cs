using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models;

public class ProductParamValue : IDeletable
{
    public Guid ProductId { get; set; }

    public int ParamId { get; set; }

    public Param Param { get; set; }

    public bool IsDeleted { get; set; }

    public string Value { get; set; }
}
