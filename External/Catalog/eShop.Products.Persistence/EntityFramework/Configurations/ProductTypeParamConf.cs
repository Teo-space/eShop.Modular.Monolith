using eShop.Products.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

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
