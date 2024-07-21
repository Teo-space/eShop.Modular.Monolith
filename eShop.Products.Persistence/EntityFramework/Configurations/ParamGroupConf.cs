using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ParamGroupConf : IEntityTypeConfiguration<ParamGroup>
{
    public void Configure(EntityTypeBuilder<ParamGroup> builder)
    {
        builder.HasKey(x => x.ParamGroupId);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
