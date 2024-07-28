namespace eShop.Products.Models.Catalog.ProductCart;

/// <summary>
/// Карточка товара
/// </summary>
public class ProductCart
{
    public int ProductId { get; set; }

    public int MakerId { get; set; }
    public string MakerName { get; set; }

    public string Number { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

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
    public double Stars { get; set; }

    public IReadOnlyCollection<ParamGroupModel> ParamGroups { get; set; } = Array.Empty<ParamGroupModel>();
}

