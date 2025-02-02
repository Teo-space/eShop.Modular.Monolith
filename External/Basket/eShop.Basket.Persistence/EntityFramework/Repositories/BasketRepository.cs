using eShop.Basket.Domain;
using eShop.Basket.Interfaces.Repositories;
using eShop.Basket.Persistence.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace eShop.Basket.Persistence.EntityFramework.Repositories;

internal class BasketRepository(BasketDbContext basketDbContext) : IBasketRepository
{
    public async Task<Result<BasketPosition>> Get(long clientId, int productId)
    {
        var basketPosition = await basketDbContext.BasketPositions
            .AsNoTracking()
            .FirstOrDefaultAsync(bp => bp.ClientId == clientId && bp.ProductId == productId);

        if (basketPosition == null)
        {
            return Results.NotFound<BasketPosition>($"Позиция козины ({clientId}, {productId}) не найдена");
        }

        return basketPosition;
    }

    public async Task<IReadOnlyCollection<BasketPosition>> GetAll(long clientId)
    {
        var basketPositions = await basketDbContext.BasketPositions
            .AsNoTracking()
            .Where(bp => bp.ClientId == clientId)
            .ToArrayAsync();

        return basketPositions;
    }

    public async Task<Result<BasketPosition>> Add(long clientId, int productId, string productName, double price, int quantity)
    {
        if (await basketDbContext.BasketPositions.AnyAsync(bp => bp.ClientId == clientId && bp.ProductId == productId))
        {
            return Results.Conflict<BasketPosition>($"Позиция козины ({clientId}, {productId}) уже существует");
        }

        var basketPosition = new BasketPosition
        {
            ClientId = clientId,
            ProductId = productId,
            ProductName = productName,
            Price = price,
            Quantity = quantity,
            Updated = DateTime.UtcNow
        };

        basketDbContext.BasketPositions.Add(basketPosition);

        await basketDbContext.SaveChangesAsync();

        return basketPosition;
    }

    public async Task<Result<BasketPosition>> UpdateQuantity(long clientId, int productId, int quantity)
    {
        var basketPosition = await basketDbContext.BasketPositions
            .FirstOrDefaultAsync(bp => bp.ClientId == clientId && bp.ProductId == productId);

        if (basketPosition == null)
        {
            return Results.NotFound<BasketPosition>($"Позиция козины ({clientId}, {productId}) не найдена");
        }

        basketPosition.Quantity = quantity;

        await basketDbContext.SaveChangesAsync();

        return basketPosition;
    }

    public async Task<Result<BasketPosition>> Remove(long clientId, int productId)
    {
        var basketPosition = await basketDbContext.BasketPositions
            .FirstOrDefaultAsync(bp => bp.ClientId == clientId && bp.ProductId == productId);

        if (basketPosition == null)
        {
            return Results.NotFound<BasketPosition>($"Позиция козины ({clientId}, {productId}) не найдена");
        }

        basketDbContext.BasketPositions.Remove(basketPosition);

        await basketDbContext.SaveChangesAsync();

        return basketPosition;
    }
}
