using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.ComplexProperty(x => x.Filters, filters =>
        {
            filters.IsRequired();
            filters.Property(x => x.SalesCount).IsRequired();
            filters.Property(x => x.Stars).IsRequired();
            filters.Property(x => x.StarsCount).IsRequired();
            filters.Property(x => x.ReviewsCount).IsRequired();
        });

    }
}
