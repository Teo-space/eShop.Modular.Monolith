using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class MakerConf : IEntityTypeConfiguration<Maker>
{
    public void Configure(EntityTypeBuilder<Maker> builder)
    {
        builder.HasKey(x => x.MakerId);

        builder.Property(x => x.Name).HasMaxLength(64);
    }
}
