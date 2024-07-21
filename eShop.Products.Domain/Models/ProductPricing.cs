namespace eShop.Products.Domain.Models;

/// <summary>
/// Расценка товара
/// (пока не используется)
/// </summary>
public class ProductPricing
{
    public Guid ProductId { get; set; }
    public int RegionId { get; set; }
    public decimal Price { get; set; }
}
