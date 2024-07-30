using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ParamConf : IEntityTypeConfiguration<Param>
{
    public void Configure(EntityTypeBuilder<Param> builder)
    {
        builder.HasKey(x => x.ParamId);
        builder.HasIndex(x => x.ParamGroupId);

        builder.Property(x => x.Name).HasMaxLength(64);
        builder.Property(x => x.Description).HasMaxLength(4096);
    }
}
