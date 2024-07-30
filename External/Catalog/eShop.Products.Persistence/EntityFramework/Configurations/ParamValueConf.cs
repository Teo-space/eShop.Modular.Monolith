using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ParamValueConf : IEntityTypeConfiguration<ParamValue>
{
    public void Configure(EntityTypeBuilder<ParamValue> builder)
    {
        builder.HasKey(x => new
        {
            x.ParamId,
            x.Value,
        });

        builder.Property(x => x.Value).HasMaxLength(16);
    }
}
