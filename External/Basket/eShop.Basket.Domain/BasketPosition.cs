namespace eShop.Basket.Domain;

public sealed class BasketPosition
{
    public long ClientId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }

    public double Price { get; set; }

    public int Quantity { get; set; }

    public DateTime Updated { get; set; }

    //Промоакции??
}
