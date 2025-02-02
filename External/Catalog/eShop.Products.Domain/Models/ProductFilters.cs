namespace eShop.Products.Domain.Models;

public sealed record ProductFilters
{
    /// <summary>
    /// Количество продаж
    /// </summary>
    public double SalesCount { get; set; }
    /// <summary>
    /// Средняя оценка в отзывах
    /// </summary>
    public double Stars { get; set; }
    /// <summary>
    /// Количество оценок
    /// </summary>
    public int StarsCount { get; set; }
    /// <summary>
    /// Количество отзывов
    /// </summary>
    public int ReviewsCount { get; set; }
}
