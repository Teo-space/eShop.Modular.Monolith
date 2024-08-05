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
    public Task<Client> GetById(long clientId);
    /// <summary>
    /// Получить клиента по номеру телефона
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public Task<Client> GetByPhone(long phone);
    /// <summary>
    /// Получить склиента по адресу почты
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<Client> GetByEmail(string email);


    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public Task<Client> Create(long phone, string userName, string firstName, string lastName, string patronymic);

    /// <summary>
    /// смена основных данных
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public Task<Client> UpdateNames(long clientId, string firstName, string lastName, string patronymic);


    /// <summary>
    /// Смена номера телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="phone"></param>
    /// <returns>Ulid token id for Accept</returns>
    public Task<Ulid> UpdatePhone(long clientId, long phone);

    /// <summary>
    /// Смена адреса почты
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="email"></param>
    /// <returns>Ulid token id for Accept</returns>
    public Task<Ulid> UpdateEmail(long clientId, string email);

    /// <summary>
    /// Выполнение действия в токене
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="token"></param>
    public Task<Ulid> AcceptToken(long clientId, Ulid token);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="phone"></param>
    /// <returns></returns>
    public Task<Client> AuthByPhone(long phone);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public Task<Client> AuthByEmail(string email);
}
