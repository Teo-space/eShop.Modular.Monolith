using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ProductTypeFilterPriceConf : IEntityTypeConfiguration<ProductTypeFilterPrice>
{
    public void Configure(EntityTypeBuilder<ProductTypeFilterPrice> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductTypeId,
            x.PriceFrom,
            x.PriceTo,
        });

        builder.Ignore(x => x.Title);
    }
}
