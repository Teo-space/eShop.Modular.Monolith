using eShop.Clients.Domain.Models;

namespace eShop.Clients.Interfaces.Repositories;

public interface IClientRepository
{
    Task<Result<Client>> GetClientByIdAsync(long clientId);
    Task<Result<Client>> GetClientByPhoneAsync(long phone);
    Task<Result<Client>> GetClientByEmailAsync(string email);
    Task<Result<Client>> GetClientByUserNameAsync(string userName);

    Task<Result<T>> GetClientByIdAsync<T>(long clientId) where T : class;
    Task<Result<T>> GetClientByPhoneAsync<T>(long phone) where T : class;
    Task<Result<T>> GetClientByEmailAsync<T>(string email) where T : class;
    Task<Result<T>> GetClientByUserNameAsync<T>(string userName) where T : class;

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
