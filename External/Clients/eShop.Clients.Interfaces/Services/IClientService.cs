using eShop.Clients.Domain.Models;
using NUlid;

namespace eShop.Clients.Interfaces.Services;

public interface IClientService
{
    /// <summary>
    /// Получить клиента по ид
    /// </summary>
    /// <param name="clientId"></param>
    /// <returns></returns>
    Task<Result<Client>> GetById(long clientId);
    /// <summary>
    /// Получить клиента по номеру телефона
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    Task<Result<Client>> GetByPhone(long phone);
    /// <summary>
    /// Получить склиента по адресу почты
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Result<Client>> GetByEmail(string email);


    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    Task<Result<Client>> Create(long phone, string email, string userName,
        string firstName, string lastName, string patronymic);

    /// <summary>
    /// смена основных данных
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    Task<Result<Client>> UpdateNames(long clientId, string firstName, string lastName, string patronymic);


    /// <summary>
    /// Смена номера телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="phone"></param>
    /// <returns>Ulid token id for Accept</returns>
    Task<Result<bool>> UpdatePhone(long clientId, long phone);

    /// <summary>
    /// Смена адреса почты
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="email"></param>
    /// <returns>Ulid token id for Accept</returns>
    Task<Result<bool>> UpdateEmail(long clientId, string email);

    Task<bool> UpdatePassword(long clientId, string oldPassword, string newPassword);

    /// <summary>
    /// Выполнение действия в токене
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="tokenId"></param>
    Task<Result<bool>> AcceptToken(long clientId, int tokenId);

}
