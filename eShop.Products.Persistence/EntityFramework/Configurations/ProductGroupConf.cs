using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ProductGroupConf : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.HasKey(x => x.ProductGroupId);
        builder.HasIndex(x => x.ParentProductGroupId);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
