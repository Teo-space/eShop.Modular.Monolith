namespace eShop.Reviews.Domain;

/// <summary>
/// Отзыв на продукт
/// </summary>
public class Review
{
    /// <summary>
    /// Ид товара
    /// </summary>
    public int ProductId { get; set; }
    /// <summary>
    /// Ид отзыва
    /// </summary>
    public int ReviewId { get; set; }
    /// <summary>
    /// Дата отзыва
    /// </summary>
    public DateTime CreatedDate { get; set; }
    /// <summary>
    /// Ид клиента
    /// </summary>
    public long ClientId { get; set; }
    /// <summary>
    /// Имя клиента
    /// </summary>
    public string ClientName { get; set; }

    /// <summary>
    /// Прошел модерацию
    /// HasQueryFilter
    /// </summary>
    public bool IsModerated { get; set; }

    /// <summary>
    /// Достоинства
    /// </summary>
    public string Advantages { get; set; }
    /// <summary>
    /// недостатки
    /// </summary>
    public string DisAdvantages { get; set; }
    /// <summary>
    /// КОмментарий
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// Оценка
    /// </summary>
    public float Stars { get; set; }

    /// <summary>
    /// Прикрепленные к отзыву изображения (URLS)
    /// </summary>
    public ReviewImages Images { get; set; }
}
