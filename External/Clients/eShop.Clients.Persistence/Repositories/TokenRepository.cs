using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.DbContexts;
using eShop.Clients.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace eShop.Clients.Persistence.Repositories;

internal class TokenRepository(IClientsDbContext clientsDbContext, IClientRepository clientRepository) : ITokenRepository
{
    public async Task<Result<ClientToken>> GetTokenAsync(long clientId, int tokenId)
    {
        var token = await clientsDbContext.ClientTokens
            .Where(x => x.ClientId == clientId && x.TokenId == tokenId)
            .FirstOrDefaultAsync();

        if (token == null)
        {
            return Results.NotFound<ClientToken>($"Токен ('{clientId}', '{tokenId}') не найден");
        }
        if (token.IsUsed)
        {
            return Results.InvalidOperation<ClientToken>($"Токен ('{clientId}', '{tokenId}') уже использован");
        }
        if (token.ValidFrom < DateTime.Now || token.ValidTo > DateTime.Now)
        {
            return Results.InvalidOperation<ClientToken>($"Токен ('{clientId}', '{tokenId}') не актуален или устарел");
        }

        return token;
    }

    public async Task<Result<int>> CreateAsync(long clientId, int tokenType, string value)
    {
        if(await clientsDbContext.ClientTokens
            .Where(x => x.ClientId == clientId)
            .Where(x => x.Created > DateTime.Now.AddMinutes(-1))
            .AnyAsync())
        {
            return Results.InvalidOperation<int>($"Операции можно выполнять не чаще чем раз в минуту");
        }

        var random = new Random();

        int tokenId = random.Next(111_111, 999_999);
        //старые токены должны очищатся раз в час \ сутки
        while (await clientsDbContext.ClientTokens.AnyAsync(x => x.ClientId == clientId && x.TokenId == tokenId))
        {
            tokenId = random.Next(111_111, 999_999);
        }

        var token = new ClientToken
        {
            ClientId = clientId,
            TokenId = tokenId,
            TokenType = tokenType,
            Value = value
        };

        clientsDbContext.ClientTokens.Add(token);
        await clientsDbContext.SaveChangesAsync();

        return token.TokenId;
    }

    public async Task MarkAsUsedAsync(ClientToken clientToken)
    {
        clientToken.IsUsed = true;
        await clientsDbContext.SaveChangesAsync();
    }

}
