namespace eShop.Order.Domain.Models.Deliveries;

/// <summary>
/// Типы доставок
/// </summary>
public enum DeliveryTypes
{
    /// <summary>
    /// Самовывоз из магазина
    /// </summary>
    StorePickup = 1,

    /// <summary>
    /// Доставка курьером
    /// </summary>
    CourierDelivery = 2
}
