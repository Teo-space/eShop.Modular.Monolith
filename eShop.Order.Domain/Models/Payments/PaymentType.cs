namespace eShop.Order.Domain.Models.Payments;


/// <summary>
/// Способ оплаты
/// </summary>
public class PaymentType
{
    /// <summary>
    /// Id типа оплаты
    /// </summary>
    public int PaymentTypeId { get; set; }
    /// <summary>
    /// наименование типа оплаты
    /// </summary>
    public string Name { get; set; }
}
