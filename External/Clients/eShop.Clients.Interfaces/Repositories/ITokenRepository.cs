using eShop.Clients.Domain.Models;

namespace eShop.Clients.Interfaces.Repositories;

public interface ITokenRepository
{
    Task<Result<ClientToken>> GetTokenAsync(long clientId, int tokenId);

    Task<Result<int>> CreateAsync(long clientId, int tokenType, string value);

    Task MarkAsUsedAsync(ClientToken clientToken);
}
