using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models.Params;

public class ProductParamValue : IDeletable
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ParamId { get; set; }

    public ProductTypeParam Param { get; set; }

    public bool IsDeleted { get; set; }

    public string Value { get; set; }
}
