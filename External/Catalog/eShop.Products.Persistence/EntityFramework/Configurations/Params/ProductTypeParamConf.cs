using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using eShop.Products.Domain.Models.Params;

namespace eShop.Products.Persistence.EntityFramework.Configurations.Params;

internal class ProductTypeParamConf : IEntityTypeConfiguration<ProductTypeParam>
{
    public void Configure(EntityTypeBuilder<ProductTypeParam> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductTypeId,
            x.ParamId,
        });

        builder.HasIndex(x => x.ProductTypeId);
    }
}
