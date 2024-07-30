using Pizzeria.Domain.Orders;

namespace eShop.Order.Domain.Models.Orders;

public class Order
{
    public long ClientId { get; set; }
    public long OrderId { get; set; }

    public DateTime CreatedAt {  get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; private set; }


    /// <summary>
    /// Информация о клиенте
    /// </summary>
    public OrderClient Client { get; private set; }

    /// <summary>
    /// Информация о доставке
    /// </summary>
    public OrderDelivery Delivery { get; private set; }

    /// <summary>
    /// Оплата
    /// </summary>
    public OrderPayment Payment { get; private set; }


    public HashSet<OrderPosition> Positions { get; private set; } = new HashSet<OrderPosition>();
}
