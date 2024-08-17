using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ProductFilterPriceConf : IEntityTypeConfiguration<ProductFilterPrice>
{
    public void Configure(EntityTypeBuilder<ProductFilterPrice> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductId,
            x.PriceFrom,
            x.PriceTo,
        });

        builder.Ignore(x => x.Title);
    }
}
