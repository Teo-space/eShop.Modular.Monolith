﻿using eShop.Basket.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eShop.Basket.Persistence.EntityFramework.DbContexts;

internal class BasketDbContext : DbContext
{
    public DbSet<BasketPosition> BasketPositions { get; init; }


    public BasketDbContext(DbContextOptions options) : base(options)
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
