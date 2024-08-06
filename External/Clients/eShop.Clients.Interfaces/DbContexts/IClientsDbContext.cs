using eShop.Clients.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eShop.Clients.Interfaces.DbContexts;

public interface IClientsDbContext : IBaseDbContext
{
    DbSet<Client> Clients { get; }

    DbSet<ClientToken> ClientTokens { get; }

    DbSet<RefreshToken> RefreshTokens { get; }

}
