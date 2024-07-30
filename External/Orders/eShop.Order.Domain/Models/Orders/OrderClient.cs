namespace eShop.Order.Domain.Models.Orders;

/// <summary>
/// Покупатель заказа
/// </summary>
public record OrderClient
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required long Phone { get; set; }
    /// <summary>
    /// адрес эл. почты
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Имя
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Фамилия
    /// </summary>
    public required string SurName { get; set; }
    /// <summary>
    /// Отчество
    /// </summary>
    public required string Patronymic { get; set; }
}
