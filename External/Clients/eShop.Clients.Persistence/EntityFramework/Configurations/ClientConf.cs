using eShop.Clients.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Clients.Persistence.EntityFramework.Configurations;

internal class ClientConf : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.ClientId);

        builder.HasIndex(x => x.Phone).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.UserName).IsUnique();

        builder.Property(x => x.Email).HasMaxLength(100);
        builder.Property(x => x.UserName).HasMaxLength(100);

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Property(x => x.Patronymic).HasMaxLength(100);

        builder.OwnsOne(x => x.Password, password =>
        {
            password.Property(p => p.Hash).HasMaxLength(64);
            password.Property(p => p.Salt).HasMaxLength(64);
        });
    }
}
