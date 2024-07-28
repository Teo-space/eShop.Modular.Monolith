namespace eShop.Products.Models.Catalog.ProductList;

public sealed class ProductModel
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
    public decimal Price { get; set; }
    /// <summary>
    /// Наличие в магазинах
    /// </summary>
    public int Availability { get; set; }

}
