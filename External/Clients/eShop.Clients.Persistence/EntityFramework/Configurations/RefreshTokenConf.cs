using eShop.Clients.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace eShop.Clients.Persistence.EntityFramework.Configurations;


internal class RefreshTokenConf : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.RefreshTokenId);
        builder.HasIndex(x => x.ClientId);

    }
}
