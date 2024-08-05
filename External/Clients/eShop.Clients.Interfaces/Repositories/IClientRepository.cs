using eShop.Clients.Domain.Models;

namespace eShop.Clients.Interfaces.Repositories;

public interface IClientRepository
{
    Task<Client> GetClientByIdAsync(long clientId);
    Task<Client> GetClientByPhoneAsync(long phone);
    Task<Client> GetClientByEmailAsync(string email);
    Task<Client> GetClientByUserNameAsync(string userName);

    Task<bool> ExistsClientByIdAsync(long clientId);
    Task<bool> ExistsClientByPhoneAsync(long phone);
    Task<bool> ExistsClientByEmailAsync(string email);
    Task<bool> ExistsClientByUserNameAsync(string userName);

    Task<Result<Client>> CreateAsync(long phone, string email, string userName,
        string firstName, string lastName, string patronymic);

    Task<Result<Client>> UpdatePhoneAsync(long clientId, long phone);
    Task<Result<Client>> UpdateEmailAsync(long clientId, string email);
    Task<Result<Client>> UpdateNamesAsync(long clientId, string firstName, string lastName, string patronymic);
    Task<Result<Client>> UpdatePasswordAsync(long clientId, Password password);


}
