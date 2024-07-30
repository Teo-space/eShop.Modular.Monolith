using eShop.Order.Domain.Models.Deliveries;

namespace eShop.Order.Domain.Models.Orders;

/// <summary>
/// Информация о доставке
/// </summary>
public record OrderDelivery
{
    /// <summary>
    /// ID типа доставки
    /// </summary>
    public required int TypeId { get; set; }

    /// <summary>
    /// Статус доставки
    /// </summary>
    public required DeliveryStatuses Status { get; set; }

    /// <summary>
    /// Доставлен
    /// </summary>
    public bool IsDelivered { get; set; }

    /// <summary>
    /// Дата начала доставки
    /// </summary>
    public DateTime Start { get; set; }
    /// <summary>
    /// Дата окончания
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required OrderDeliveryAddress Address { get; set; }
}
