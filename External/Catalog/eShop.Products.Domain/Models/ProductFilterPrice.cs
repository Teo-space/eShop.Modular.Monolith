namespace eShop.Products.Domain.Models;

public sealed class ProductFilterPrice
{
    public int ProductId { get; set; }

    public double PriceFrom { get; set; }
    public double PriceTo { get; set; }

    public string Title => $"{PriceFrom} - {PriceTo}";
}
