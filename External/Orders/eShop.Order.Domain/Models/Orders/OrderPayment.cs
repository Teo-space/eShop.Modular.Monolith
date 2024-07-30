using eShop.Order.Domain.Models.Payments;

namespace Pizzeria.Domain.Orders;

/// <summary>
/// Платеж заказа
/// </summary>
public record OrderPayment
{
    /// <summary>
    /// Способ оплаты
    /// </summary>
    public required PaymentTypes Type { get; set; }

    /// <summary>
    /// Сумма платежа
    /// </summary>
    public decimal Sum { get; private set; }

    /// <summary>
    /// Дата платежа
    /// </summary>
    public DateTime PayedAt { get; private set; }
    /// <summary>
    /// Оплачено
    /// </summary>
    public bool IsPayed { get; private set; }



    public void MakePayment(PaymentTypes paymentType)
    {
        IsPayed = true;
        PayedAt = DateTime.Now;
        Type = paymentType;
    }
}
