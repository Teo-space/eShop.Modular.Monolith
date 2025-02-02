using eShop.Products.Domain.Interfaces;

namespace eShop.Products.Domain.Models.Params;

/// <summary>
/// Список возможных значений параметров
/// </summary>
public class ParamValue : IDeletable
{
    public int ParamId { get; set; }

    public string Value { get; set; }

    public bool IsDeleted { get; set; }
}
