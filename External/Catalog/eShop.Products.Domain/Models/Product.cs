using eShop.Products.Domain.Interfaces;
using eShop.Products.Domain.Models.Params;

namespace eShop.Products.Domain.Models;

public sealed class Product : IDeletable
{
    public int ProductId { get; set; }
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }

    public bool IsDeleted { get; set; }

    public int MakerId { get; set; }
    public Maker Maker { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ProductFilters Filters { get; set; } = new ProductFilters();

    /// <summary>
    /// Наличие на складах
    /// </summary>
    public List<ProductWarehouse> Warehouses { get; private set; } = new List<ProductWarehouse>();
    /// <summary>
    /// Значения параметров
    /// </summary>
    public List<ProductParamValue> ParamValues { get; private set; } = new List<ProductParamValue>();
}
