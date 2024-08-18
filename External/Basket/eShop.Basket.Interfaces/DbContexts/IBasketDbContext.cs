using eShop.Basket.Domain;
using Microsoft.EntityFrameworkCore;

namespace eShop.Basket.Interfaces.DbContexts;

public interface IBasketDbContext : IBaseDbContext
{
    public DbSet<BasketPosition> BasketPositions { get; }
}
