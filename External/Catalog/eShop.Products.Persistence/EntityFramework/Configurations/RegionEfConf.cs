using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class RegionEfConf : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(x => x.RegionId);

        builder.Property(x => x.Name).HasMaxLength(64);
    }
}
