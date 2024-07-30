namespace eShop.Order.Domain.Models.Deliveries;


/// <summary>
/// Статусы доставки
/// </summary>
public class DeliveryStatus 
{
    /// <summary>
    /// ID статуса доставки
    /// </summary>
    public int DeliveryStatusId { get; set; }
    /// <summary>
    /// Наименование статуса доставки
    /// </summary>
    public string Name { get; set; }
}
