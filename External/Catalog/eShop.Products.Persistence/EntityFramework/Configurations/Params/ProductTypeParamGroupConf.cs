using eShop.Products.Domain.Models.Params;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations.Params;

internal class ProductTypeParamGroupConf : IEntityTypeConfiguration<ProductTypeParamGroup>
{
    public void Configure(EntityTypeBuilder<ProductTypeParamGroup> builder)
    {
        builder.HasKey(x => x.ParamGroupId);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
