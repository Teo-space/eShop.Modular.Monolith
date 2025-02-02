using eShop.Basket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
