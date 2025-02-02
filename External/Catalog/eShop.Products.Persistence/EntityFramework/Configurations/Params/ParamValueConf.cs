using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eShop.Products.Domain.Models.Params;

namespace eShop.Products.Persistence.EntityFramework.Configurations.Params;

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
