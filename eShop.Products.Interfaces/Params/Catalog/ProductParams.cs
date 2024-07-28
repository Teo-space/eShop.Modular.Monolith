using eShop.Products.Interfaces.Enum;

namespace eShop.Products.Interfaces.Params.Catalog;

public record ProductParams
{
    public int ProductTypeId { get; set; }

    public ProductsSorting ProductsSorting { get; set; } = ProductsSorting.PriceAscending;

    public IReadOnlyCollection<int> Makers { get; set; } = Array.Empty<int>();

    public IReadOnlyCollection<ProductFilterParam> Params { get; set; } = Array.Empty<ProductFilterParam>();

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    //Наличие: В наличии, сегодня, завтра

    public bool HasStars { get; set; }
    public bool HasReviews { get; set; }
}