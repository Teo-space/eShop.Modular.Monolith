using eShop.Clients.Domain;
using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.Repositories;
using eShop.Clients.Interfaces.ServicesExternal;
using System.Text.Json;

namespace eShop.Clients.Services;

internal class ClientService(
    IClientRepository clientRepository, 
    ITokenRepository tokenRepository,
    IEmailSender emailSender,
    ISmsSender smsSender)
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
    /// <returns>token id for Accept</returns>
    public async Task<Result<bool>> UpdatePhone(long clientId, long phone)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if (client == null)
        {
            return Results.NotFound<bool>($"Клиент '{clientId}' не найден");
        }
        if (client.Phone == phone)
        {
            return Results.Conflict<bool>($"Клиент '{clientId}' уже имеет номер телефона '{phone}'");
        }

        var tokenResult = await tokenRepository.CreateAsync(clientId, TokenTypes.AcceptPhone, phone.ToString());
        if(!tokenResult.Success)
        {
            return new Result<bool>
            {
                Value = false,
                Success = tokenResult.Success,
                Type = tokenResult.Type,
                Detail = tokenResult.Detail,
                Errors = tokenResult.Errors
            };
        }

        await emailSender.SendAsync(client.Email,
            "[eShop] Смена номера телефона",
            $@"Код для смены номера телефона: '{tokenResult.Value}'");

        await smsSender.SendAsync(client.Phone, $@"Код для смены номера телефона: '{tokenResult.Value}'");

        return true;
    }

    /// <summary>
    /// Смена адреса почты
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="email"></param>
    /// <returns>token id for Accept</returns>
    public async Task<Result<bool>> UpdateEmail(long clientId, string email)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if (client == null)
        {
            return Results.NotFound<bool>($"Клиент '{clientId}' не найден");
        }
        if (client.Email == email)
        {
            return Results.Conflict<bool>($"Клиент '{clientId}' уже имеет адрес почты '{email}'");
        }

        var tokenResult = await tokenRepository.CreateAsync(clientId, TokenTypes.AcceptEmail, email);

        if (!tokenResult.Success)
        {
            return new Result<bool>
            {
                Value = false,
                Success = tokenResult.Success,
                Type = tokenResult.Type,
                Detail = tokenResult.Detail,
                Errors = tokenResult.Errors
            };
        }

        await emailSender.SendAsync(client.Email,
            "[eShop] Смена почтового адреса",
            $@"Код для смены почтового адреса: '{tokenResult.Value}'");

        await smsSender.SendAsync(client.Phone, $@"Код для смены почтового адреса: '{tokenResult.Value}'");

        return true;
    }

    public async Task<bool> UpdatePassword(long clientId, string oldPassword, string newPassword)
    {
        var client = await clientRepository.GetClientByIdAsync(clientId);
        if(client == null)
        {
            return Results.NotFound<bool>($"Клиент '{clientId}' не найден");
        }
        if(!PasswordHasher.String.Verify(oldPassword, client.Password.Hash, client.Password.Salt))
        {
            return Results.InvalidOperation<bool>($"Старый пароль введен неверно");
        }
        PasswordHasher.String.HashedPassword hashedPassword = PasswordHasher.String.Hash(newPassword);
        string hashedPasswordJson = JsonSerializer.Serialize(hashedPassword);

        var tokenResult = await tokenRepository.CreateAsync(clientId, TokenTypes.ChangePassword, hashedPasswordJson);

        if (!tokenResult.Success)
        {
            return tokenResult.MapTo<int, bool>();
        }

        await emailSender.SendAsync(client.Email,
            "[eShop] Смена пароля",
            $@"Код для смены пароля: '{tokenResult.Value}'");

        await smsSender.SendAsync(client.Phone, $@"Код для смены пароля: '{tokenResult.Value}'");

        return true;
    }

    /// <summary>
    /// Выполнить токен
    /// </summary>
    public async Task<Result<bool>> AcceptToken(long clientId, int tokenId)
    {
        if (!await clientRepository.ExistsClientByIdAsync(clientId))
        {
            return Results.NotFound<bool>($"Клиент '{clientId}' не найден");
        }
        
        var tokenResult = await tokenRepository.GetTokenAsync(clientId, tokenId);
        if (!tokenResult.Success)
        {
            return Results.NotFound<bool>($"Токен ('{clientId}', '{tokenId}') не найден");
        }

        var token = tokenResult.Value;

        if (token.TokenType == TokenTypes.AcceptPhone)
        {
            long phone = long.Parse(token.Value);
            await clientRepository.UpdatePhoneAsync(clientId, phone);

            return Results.Ok<bool>(true, "Номер телефона изменен");
        }
        else if (token.TokenType == TokenTypes.AcceptEmail)
        {
            await clientRepository.UpdateEmailAsync(clientId, token.Value);

            return Results.Ok<bool>(true, "Адрес почты изменен");
        }
        else if (token.TokenType == TokenTypes.ChangePassword)
        {
            var password = JsonSerializer.Deserialize<Password>(token.Value);
            if (password != null && !string.IsNullOrEmpty(password.Hash) && !string.IsNullOrEmpty(password.Salt))
            {
                await clientRepository.UpdatePasswordAsync(clientId, password);

                return Results.Ok<bool>(true, "Пароль изменен");
            }
            else return Results.InvalidOperation<bool>("Ошибка JsonSerializer");
        }
        else
        {
            return Results.InvalidOperation<bool>(
                $"Токен({clientId}, {tokenId}) с типом '{token.TokenType}' не поддерживается в этом методе");
        }
    }


}
