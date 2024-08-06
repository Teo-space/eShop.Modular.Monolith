using eShop.Clients.Domain.Models;
using NUlid;

namespace eShop.Clients.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task<Result<RefreshToken>> GetByIdAsync(Ulid refreshTokenId);

    Task<Result<Ulid>> CreateAsync(long clientId);

    Task MarkAsUsed(RefreshToken refreshToken);
}
