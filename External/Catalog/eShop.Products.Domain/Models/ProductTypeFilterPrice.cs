namespace eShop.Products.Domain.Models;

/// <summary>
/// Список фильтров по цене
/// </summary>
public sealed class ProductTypeFilterPrice
{
    public int ProductTypeId { get; private set; }

    public int PriceFrom { get; private set; }
    public int PriceTo { get; private set; }

    public string Title => $"{PriceFrom} - {PriceTo}";

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductTypeFilterPrice() { }

    public static ProductTypeFilterPrice Create(ProductType productType, int priceFrom, int priceTo)
    {
        return new ProductTypeFilterPrice
        {
            ProductTypeId = productType.ProductTypeId,
            PriceFrom = priceFrom,
            PriceTo = priceTo
        };
    }
}
