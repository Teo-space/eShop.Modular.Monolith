using eShop.Clients.Domain;
using NUlid;

namespace eShop.Clients.Interfaces.Services;

public interface IClientService
{
    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public Task<Client> Create(long phone, string firstName, string lastName, string patronymic);
    /// <summary>
    /// Смена номера телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="phone"></param>
    /// <returns>Ulid token id for Accept</returns>
    public Task<Ulid> UpdatePhone(long clientId, long phone);
    /// <summary>
    /// Подтверждение токена смены телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Client> AcceptPhone(long clientId, Ulid token);

    /// <summary>
    /// Смена адреса почты
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="email"></param>
    /// <returns>Ulid token id for Accept</returns>
    public Task<Ulid> UpdatePhone(long clientId, string email);
    /// <summary>
    /// Подтверждение токена смены телефона
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<Client> AcceptEmail(long clientId, Ulid token);

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
    /// смена основных данных
    /// </summary>
    /// <param name="clientId"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="patronymic"></param>
    /// <returns></returns>
    public Task<Client> Update(long clientId, string firstName, string lastName, string patronymic);

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
