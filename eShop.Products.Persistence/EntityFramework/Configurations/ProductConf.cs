using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ProductConf : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.ProductId);
        builder.HasIndex(x => x.ProductTypeId);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Number).HasMaxLength(32);
        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
