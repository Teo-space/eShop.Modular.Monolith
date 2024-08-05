using eShop.Clients.Domain.Models;
using NUlid;

namespace eShop.Clients.Interfaces.Repositories;

public interface ITokenRepository
{
    Task<Result<ClientToken>> GetTokenAsync(long clientId, Ulid tokenId);

    Task<Result<Ulid>> CreateAsync(long clientId, int tokenType, string value);
}
