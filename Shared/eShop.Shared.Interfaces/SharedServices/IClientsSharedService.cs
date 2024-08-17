using eShop.Shared.Interfaces.Models.Clients;

namespace eShop.Shared.Interfaces.SharedServices;

public interface IClientsSharedService
{
    Task<Result<ClientModel>> GetClientByIdAsync(long clientId);
    Task<Result<ClientModel>> GetClientByPhoneAsync(long phone);
    Task<Result<ClientModel>> GetClientByEmailAsync(string email);
    Task<Result<ClientModel>> GetClientByUserNameAsync(string userName);

    Task<bool> ExistsClientByIdAsync(long clientId);
    Task<bool> ExistsClientByPhoneAsync(long phone);
    Task<bool> ExistsClientByEmailAsync(string email);
    Task<bool> ExistsClientByUserNameAsync(string userName);
}
