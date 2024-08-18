namespace eShop.Basket.Models;

/// <summary>
/// Товар в корзине
/// </summary>
public sealed class BasketPositionModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string Updated { get; set; }
}
