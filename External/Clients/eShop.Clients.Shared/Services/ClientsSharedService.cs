using eShop.Clients.Interfaces.Repositories;
using eShop.Shared.Interfaces.Models.Clients;
using eShop.Shared.Interfaces.SharedServices;

namespace eShop.Clients.Shared.Services;

internal class ClientsSharedService(IClientRepository clientRepository) : IClientsSharedService
{
    public Task<Result<ClientModel>> GetClientByIdAsync(long clientId)=> clientRepository.GetClientByIdAsync<ClientModel>(clientId);

    public Task<Result<ClientModel>> GetClientByPhoneAsync(long phone) => clientRepository.GetClientByIdAsync<ClientModel>(phone);

    public Task<Result<ClientModel>> GetClientByEmailAsync(string email) => clientRepository.GetClientByEmailAsync<ClientModel>(email);

    public Task<Result<ClientModel>> GetClientByUserNameAsync(string userName) => clientRepository.GetClientByUserNameAsync<ClientModel>(userName);

    public Task<bool> ExistsClientByIdAsync(long clientId) => clientRepository.ExistsClientByIdAsync(clientId);

    public Task<bool> ExistsClientByPhoneAsync(long phone) => clientRepository.ExistsClientByPhoneAsync(phone);

    public Task<bool> ExistsClientByEmailAsync(string email) => clientRepository.ExistsClientByEmailAsync(email);

    public Task<bool> ExistsClientByUserNameAsync(string userName) => clientRepository.ExistsClientByEmailAsync(userName);

}
