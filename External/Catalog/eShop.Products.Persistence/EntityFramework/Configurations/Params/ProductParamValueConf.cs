using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eShop.Products.Domain.Models.Params;

namespace eShop.Products.Persistence.EntityFramework.Configurations.Params;

internal class ProductParamValueConf : IEntityTypeConfiguration<ProductParamValue>
{
    public void Configure(EntityTypeBuilder<ProductParamValue> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductId,
            x.ParamId,
            x.Value,
        });

        builder.HasIndex(x => x.ProductId);
        builder.Property(x => x.Value).HasMaxLength(16);

    }
}
