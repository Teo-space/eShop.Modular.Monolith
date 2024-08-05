using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.DbContexts;
using eShop.Clients.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using NUlid;

namespace eShop.Clients.Persistence.Repositories;

internal class TokenRepository(IClientsDbContext clientsDbContext, IClientRepository clientRepository) : ITokenRepository
{
    public async Task<Result<ClientToken>> GetTokenAsync(long clientId, Ulid tokenId)
    {
        var token = await clientsDbContext.ClientTokens
            .Where(x => x.ClientId == clientId && x.TokenId == tokenId)
            .FirstOrDefaultAsync();

        if (token == null)
        {
            return Results.NotFound<ClientToken>($"Токен ('{clientId}', '{tokenId}') не найден");
        }

        return token;
    }

    public async Task<Result<Ulid>> CreateAsync(long clientId, int tokenType, string value)
    {
        var token = new ClientToken
        {
            ClientId = clientId,
            TokenId = Ulid.NewUlid(),
            TokenType = tokenType,
            Value = value
        };

        clientsDbContext.ClientTokens.Add(token);
        await clientsDbContext.SaveChangesAsync();

        return token.TokenId;
    }

}
