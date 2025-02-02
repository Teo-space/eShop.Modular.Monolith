namespace eShop.Products.Domain.Models;

/// <summary>
/// Расценка товара
/// (пока не используется)
/// </summary>
public sealed class ProductWarehouse
{
    public int ProductId { get; private set; }
    public int RegionId { get; private set; }

    public decimal Price { get; private set; }
    public int Quantity { get; private set; }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private ProductWarehouse() { }

    public static ProductWarehouse Create(Product product, Region region, decimal price, int quantity)
    {
        return new ProductWarehouse
        {
            ProductId = product.ProductId,
            RegionId = region.RegionId,
            Price = price,
            Quantity = quantity
        };
    }
}
