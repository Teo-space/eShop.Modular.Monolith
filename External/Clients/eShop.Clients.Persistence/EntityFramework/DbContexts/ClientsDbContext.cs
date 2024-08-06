using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eShop.Clients.Persistence.EntityFramework.DbContexts;

internal class ClientsDbContext : DbContext, IClientsDbContext
{
    public DbSet<Client> Clients { get; init; }
    public DbSet<ClientToken> ClientTokens { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }



    public ClientsDbContext(DbContextOptions options) : base(options)
    {
    }

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Console.WriteLine, minimumLevel: Microsoft.Extensions.Logging.LogLevel.Information);
    }
#endif

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
