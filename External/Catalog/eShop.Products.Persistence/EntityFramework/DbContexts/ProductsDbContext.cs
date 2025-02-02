using eShop.Products.Domain.Models;
using eShop.Products.Domain.Models.Params;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eShop.Products.Persistence.EntityFramework.DbContexts;

internal class ProductsDbContext : DbContext
{
    public DbSet<Maker> Makers { get; init; }

    public DbSet<ProductGroup> ProductGroups { get; init; }
    public DbSet<ProductType> ProductTypes { get; init; }
    public DbSet<ProductTypeParam> ProductTypeParams { get; init; }
    public DbSet<ProductTypeParamGroup> ProductTypeParamGroups { get; init; }
    public DbSet<ParamValue> ParamValues { get; init; }

    public DbSet<Product> Products { get; init; }
    public DbSet<ProductParamValue> ProductParamValues { get; init; }
    public DbSet<ProductTypeFilterPrice> ProductFilterPrices { get; init; }
    public DbSet<ProductWarehouse> ProductWarehouses { get; init; }
    public DbSet<Region> Regions { get; init; }



    public ProductsDbContext(DbContextOptions options) : base(options)
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
