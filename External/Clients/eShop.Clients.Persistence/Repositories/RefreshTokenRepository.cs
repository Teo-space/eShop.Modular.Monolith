using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.DbContexts;
using eShop.Clients.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using NUlid;

namespace eShop.Clients.Persistence.Repositories;

internal class RefreshTokenRepository(IClientsDbContext clientsDbContext) : IRefreshTokenRepository
{
    public async Task<Result<RefreshToken>> GetByIdAsync(Ulid refreshTokenId)
    {
        var refreshToken = await clientsDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.RefreshTokenId == refreshTokenId);
        if(refreshToken == null)
        {
            return Results.NotFound<RefreshToken>($"Refresh токен '{refreshTokenId}' не найден");
        }
        if(refreshToken.IsUsed)
        {
            return Results.InvalidOperation<RefreshToken>($"Refresh Token '{refreshTokenId}' уже был использован");
        }

        return refreshToken;
    }

    public async Task<Result<Ulid>> CreateAsync(long clientId)
    {
        var refreshToken = new RefreshToken()
        {
            RefreshTokenId = Ulid.NewUlid(),
            ClientId = clientId,
            IsUsed = false,
            Created = DateTime.Now
        };

        clientsDbContext.RefreshTokens.Add(refreshToken);
        await clientsDbContext.SaveChangesAsync();

        return refreshToken.RefreshTokenId;
    }

    public Task  MarkAsUsed(RefreshToken refreshToken)
    {
        refreshToken.IsUsed = true;
        return clientsDbContext.SaveChangesAsync();
    }

}
