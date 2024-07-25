using eShop.Products.Interfaces.Enum;

namespace eShop.Products.Interfaces.Params.CatalogService;

public record ProductParams
{
    public Guid ProductTypeId { get; set; }
    public ProductsSorting ProductsSorting { get; set; } = ProductsSorting.PriceAscending;

    public IReadOnlyCollection<Guid> Makers { get; set; } = Array.Empty<Guid>();

    public IReadOnlyCollection<ProductFilterParam> Params { get; set; } = Array.Empty<ProductFilterParam>();

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;

    //Наличие: В наличии, сегодня, завтра

    // HasStars

    //HasReviews
}