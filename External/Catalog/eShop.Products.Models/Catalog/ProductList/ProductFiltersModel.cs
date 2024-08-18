namespace eShop.Products.Models.Catalog.ProductList;

/// <summary>
/// Доступные фильтры
/// </summary>
public sealed class ProductFiltersModel
{
    /// <summary>
    /// Фильтр производитель - количество
    /// </summary>
    public int MakersCount { get; set; }
    /// <summary>
    /// Фильтр параметр - количество
    /// </summary>
    public int ParamsCount { get; set; }

    /// <summary>
    /// Производители
    /// </summary>
    public IReadOnlyCollection<MakerFilterModel> Makers { get; set; } = Array.Empty<MakerFilterModel>();
    /// <summary>
    /// Параметры
    /// </summary>
    public IReadOnlyCollection<ProductParamFilterModel> Params { get; set; } = Array.Empty<ProductParamFilterModel>();
}

/// <summary>
/// Фильтр производитель
/// </summary>
public sealed class MakerFilterModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Количество - товаров с данным фильтром
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Выбран
    /// </summary>
    public bool IsSelected { get; set; }
    /// <summary>
    /// Доступен
    /// </summary>
    public bool IsEnabled { get; set; }
}
/// <summary>
/// Фильтр параметр
/// </summary>
public sealed class ProductParamFilterModel
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int ParamId { get; set; }
    /// <summary>
    /// Наименование
    /// </summary>
    public string ParamName { get; set; }
    /// <summary>
    /// Выбран
    /// </summary>
    public bool IsSelected { get; set; }

    public IReadOnlyCollection<ValueFilterModel> Values { get; set; } = Array.Empty<ValueFilterModel>();
}
/// <summary>
/// Значение параметра
/// </summary>
public sealed class ValueFilterModel
{
    /// <summary>
    /// Значение параметра
    /// </summary>
    public string Value { get; set; }
    /// <summary>
    /// Количество - товаров с данным фильтром
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Выбран
    /// </summary>
    public bool IsSelected { get; set; }
    /// <summary>
    /// Доступен
    /// </summary>
    public bool IsEnabled { get; set; }
}
