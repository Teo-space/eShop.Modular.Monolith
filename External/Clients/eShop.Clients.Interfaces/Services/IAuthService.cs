using eShop.Clients.Interfaces.Models;
using NUlid;

namespace eShop.Clients.Interfaces.Services;

public interface IAuthService
{
    Task<Result<bool>> AuthByPhone(long phone);

    Task<Result<bool>> AuthByEmail(string email);

    Task<Result<AuthModel>> AcceptAuthToken(long clientId, int tokenId);

    Task<Result<AuthModel>> AuthByEmailAndPassword(string email, string password);

    Task<Result<AuthModel>> RefreshToken(Ulid refreshTokenId);
}
