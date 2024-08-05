using eShop.Clients.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Products.Persistence.EntityFramework.Configurations;

internal class ClientTokenConf : IEntityTypeConfiguration<ClientToken>
{
    public void Configure(EntityTypeBuilder<ClientToken> builder)
    {
        builder.HasKey(x => new
        {
            x.ClientId,
            x.TokenId,
        });

        builder.Property(x => x.Value).HasMaxLength(1000);

    }
}
