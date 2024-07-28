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

    public IReadOnlyCollection<ProductParamGroupModel> ParamGroups  { get; set; } = new List<ProductParamGroupModel>();
}

public sealed class ProductParamGroupModel
{
    public string ParamGroupName { get; set; }

    public IReadOnlyCollection<ProductParamValueModel> Params { get; set; } = Array.Empty<ProductParamValueModel>();
}

public sealed class ProductParamValueModel
{
    public int ParamId { get; set; }
    public string ParamName { get; set; }

    public string Value { get; set; }

}
