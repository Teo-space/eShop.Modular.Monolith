using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.Order.Domain.Models.Orders;

public class OrderPosition
{
    public long ClientId { get; set; }
    public long OrderId { get; set; }
    public Order Order { get; private set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }

    /// <summary>
    /// количество
    /// </summary>
    public int Quanity { get; private set; }
    /// <summary>
    /// Цена
    /// </summary>
    public decimal Price { get; private set; }
    /// <summary>
    /// Сумма
    /// </summary>
    [NotMapped]
    public decimal Sum { get => Quanity * Price; }
}
