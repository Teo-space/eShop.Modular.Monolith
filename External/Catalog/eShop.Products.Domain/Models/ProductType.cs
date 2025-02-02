using eShop.Products.Domain.Interfaces;
using eShop.Products.Domain.Models.Params;

namespace eShop.Products.Domain.Models;

/// <summary>
/// Тип товара внутры группы товаров
/// Например:
/// Бытовая техника \ Холодильники
/// </summary>
public sealed class ProductType : IDeletable
{
    public int ProductTypeId { get; set; }

    public int ProductGroupId { get; set; }

    public bool IsDeleted { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// Список фильтров по цене для типа товара
    /// </summary>
    public List<ProductTypeFilterPrice> FilterPrices { get; private set; } = new List<ProductTypeFilterPrice>();

    /// <summary>
    /// Товары
    /// </summary>
    public List<Product> Products { get; private set; } = new List<Product>();

    /// <summary>
    /// Параметры типа товаров
    /// </summary>
    public List<ProductTypeParam> Params { get; private set; } = new List<ProductTypeParam>();

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductType() { }

    public static ProductType Create(ProductGroup productGroup, string name, string description)
    {
        return new ProductType
        {
            ProductGroupId = productGroup.ProductGroupId,
            IsDeleted = false,
            Name = name,
            Description = description
        };
    }

    public static ProductType CreateExists(int productTypeId, ProductGroup productGroup, string name, string description)
    {
        return new ProductType
        {
            ProductTypeId = productTypeId,
            ProductGroupId = productGroup.ProductGroupId,
            IsDeleted = false,
            Name = name,
            Description = description
        };
    }


}
