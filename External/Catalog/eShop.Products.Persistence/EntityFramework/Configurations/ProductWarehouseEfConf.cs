using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ProductWarehouseEfConf : IEntityTypeConfiguration<ProductWarehouse>
{
    public void Configure(EntityTypeBuilder<ProductWarehouse> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductId,
            x.RegionId,
        });
    }
}
