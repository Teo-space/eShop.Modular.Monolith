namespace eShop.Shared.Interfaces.Models.Products;

public sealed record ProductModel
{
    public int ProductId { get; set; }
    public string ProductNumber { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductTypeId { get; set; }
    public string ProductTypeName { get; set; }

    public int MakerId { get; set; }
    public string MakerName { get; set; }


    /// <summary>
    /// Цена
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// Наличие в магазинах
    /// </summary>
    public int Availability { get; set; }

    /// <summary>
    /// Средняя оценка в отзывах
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
    /// 
    /// </summary>
    public int ReviewsCount { get; set; }
}
