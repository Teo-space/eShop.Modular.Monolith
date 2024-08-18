using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eShop.Basket.Domain;
using NUlid;

namespace eShop.Basket.Persistence.EntityFramework.Configurations;


internal class BasketPositionConf : IEntityTypeConfiguration<BasketPosition>
{
    public void Configure(EntityTypeBuilder<BasketPosition> builder)
    {
        builder.HasKey(x => new
        {
            x.ClientId,
            x.ProductId,
        });

        builder.Property(x => x.ProductName).HasMaxLength(64);
    }
}
