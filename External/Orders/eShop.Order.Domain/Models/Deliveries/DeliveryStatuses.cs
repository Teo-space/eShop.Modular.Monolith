namespace eShop.Order.Domain.Models.Deliveries;

/// <summary>
/// Статусы доставки
/// </summary>
public enum DeliveryStatuses
{
    /// <summary>
    /// Ожидает
    /// </summary>
    Pending = 0,
    /// <summary>
    /// Взят в работу
    /// </summary>
    InWork = 100,
    /// <summary>
    /// В процессе доставки
    /// </summary>
    Delivering = 200,
    /// <summary>
    /// Доставлен
    /// </summary>
    Ready = 1000,
    /// <summary>
    /// Отменен
    /// </summary>
    Canceled = 10000,
}
