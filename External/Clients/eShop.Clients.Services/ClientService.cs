using eShop.Clients.Domain;
using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.Repositories;
using NUlid;
using System.Numerics;
using System.Text.Json;

namespace eShop.Clients.Services;

internal class ClientService(IClientRepository clientRepository, ITokenRepository tokenRepository)
{
    /// <summary>
    /// Получить клиента по ид
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    public Task<Client> GetById(long clientId) => clientRepository.GetClientByIdAsync(clientId);
    /// <summary>
    /// Получить клиента по номеру телефона
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public Task<Client> GetByPhone(long phone) => clientRepository.GetClientByPhoneAsync(phone);
    /// <summary>
    /// Получить склиента по адресу почты
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<Client> GetByEmail(string email) => clientRepository.GetClientByEmailAsync(email);

    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public async Task<Client> Create(long phone, string email, string userName, 
        string firstName, string lastName, string patronymic)
    {
        if (await clientRepository.ExistsClientByPhoneAsync(phone))
        {
            return Results.Conflict<Client>($"Пользователь с таким номером телефона уже существует");
        }
        if (await clientRepository.ExistsClientByEmailAsync(email))
        {
            return Results.Conflict<Client>($"Пользователь с таким адресом почты уже существует");
        }
        if (await clientRepository.ExistsClientByUserNameAsync(userName))
        {
            return Results.Conflict<Client>($"Пользователь с таким именем уже существует");
        }

        return await clientRepository.CreateAsync(phone, email, userName, firstName, lastName, patronymic);
    }

    /// <summary>
    /// смена основных данных
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public async Task<Result<Client>> UpdateNames(long clientId, string firstName, string lastName, string patronymic)
    {
        return await clientRepository.UpdateNamesAsync(clientId, firstName, lastName, patronymic);
    }

    /// <summary>
    /// Смена номера телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="phone"></param>
    /// <returns>Ulid token id for Accept</returns>
    public async Task<Result<Ulid>> UpdatePhone(long clientId, long phone)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if (client == null)
        {
            return Results.NotFound<Ulid>($"Клиент '{clientId}' не найден");
        }
        if (client.Phone == phone)
        {
            return Results.Conflict<Ulid>($"Клиент '{clientId}' уже имеет номер телефона '{phone}'");
        }

        return await tokenRepository.CreateAsync(clientId, TokenTypes.AcceptPhone, phone.ToString());
    }

    /// <summary>
    /// Смена адреса почты
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="email"></param>
    /// <returns>Ulid token id for Accept</returns>
    public async Task<Ulid> UpdateEmail(long clientId, string email)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if (client == null)
        {
            return Results.NotFound<Ulid>($"Клиент '{clientId}' не найден");
        }
        if (client.Email == email)
        {
            return Results.Conflict<Ulid>($"Клиент '{clientId}' уже имеет адрес почты '{email}'");
        }

        return await tokenRepository.CreateAsync(clientId, TokenTypes.AcceptEmail, email);
    }

    public async Task<Ulid> UpdatePassword(long clientId, string oldPassword, string newPassword)
    {
        //generate hash + salt
    }

    /// <summary>
    /// Выполнить токен
    /// </summary>
    public async Task<Result<Ulid>> AcceptToken(long clientId, Ulid tokenId)
    {
        if (!await clientRepository.ExistsClientByIdAsync(clientId))
        {
            return Results.NotFound<Ulid>($"Клиент '{clientId}' не найден");
        }
        
        var tokenResult = await tokenRepository.GetTokenAsync(clientId, tokenId);
        if (!tokenResult.Success)
        {
            return Results.NotFound<Ulid>($"Токен ('{clientId}', '{tokenId}') не найден");
        }

        var token = tokenResult.Value;

        if (token.TokenType == TokenTypes.AcceptPhone)
        {
            long phone = long.Parse(token.Value);
            await clientRepository.UpdatePhoneAsync(clientId, phone);
        }
        else if (token.TokenType == TokenTypes.AcceptEmail)
        {
            await clientRepository.UpdateEmailAsync(clientId, token.Value);
        }
        else if (token.TokenType == TokenTypes.ChangePassword)
        {
            var password = JsonSerializer.Deserialize<Password>(token.Value);
            if (password != null && !string.IsNullOrEmpty(password.Hash) && !string.IsNullOrEmpty(password.Salt))
            {
                await clientRepository.UpdatePasswordAsync(clientId, password);
            }
        }

        return token.TokenId;
    }

}
