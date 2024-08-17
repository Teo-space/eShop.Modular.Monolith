using eShop.Clients.Domain.Models;
using eShop.Clients.Interfaces.DbContexts;
using eShop.Clients.Interfaces.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace eShop.Clients.Persistence.Repositories;

internal class ClientRepository(IClientsDbContext clientsDbContext) : IClientRepository
{
    public async Task<Result<Client>> GetClientByIdAsync(long clientId) 
        => await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId) 
        ?? Results.NotFound<Client>($"Клиент не найден по ID: {clientId}");

    public async Task<Result<Client>> GetClientByPhoneAsync(long phone) 
        => await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.Phone == phone)
        ?? Results.NotFound<Client>($"Клиент не найден по номеру телефона: {phone}");

    public async Task<Result<Client>> GetClientByEmailAsync(string email) 
        => await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.Email == email)
        ?? Results.NotFound<Client>($"Клиент не найден по email: {email}");

    public async Task<Result<Client>> GetClientByUserNameAsync(string userName) 
        => await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.UserName == userName)
        ?? Results.NotFound<Client>($"Клиент не найден по UserName: {userName}");

    public async Task<Result<T>> GetClientByIdAsync<T>(long clientId) where T: class
        => await clientsDbContext.Clients
        .Where(x => x.ClientId == clientId)
        .ProjectToType<T>()
        .FirstOrDefaultAsync() ?? Results.NotFound<T>($"Клиент не найден по ID: {clientId}");

    public async Task<Result<T>> GetClientByPhoneAsync<T>(long phone) where T : class
        => await clientsDbContext.Clients
        .Where(x => x.Phone == phone)
        .ProjectToType<T>()
        .FirstOrDefaultAsync() ?? Results.NotFound<T>($"Клиент не найден по номеру телефона: {phone}");

    public async Task<Result<T>> GetClientByEmailAsync<T>(string email) where T : class
        => await clientsDbContext.Clients
        .Where(x => x.Email == email)
        .ProjectToType<T>()
        .FirstOrDefaultAsync() ?? Results.NotFound<T>($"Клиент не найден по email: {email}");

    public async Task<Result<T>> GetClientByUserNameAsync<T>(string userName) where T : class
        => await clientsDbContext.Clients
        .Where(x => x.UserName == userName)
        .ProjectToType<T>()
        .FirstOrDefaultAsync() ?? Results.NotFound<T>($"Клиент не найден по UserName: {userName}");


    public Task<bool> ExistsClientByIdAsync(long clientId) => clientsDbContext.Clients.AnyAsync(x => x.ClientId == clientId);
    public Task<bool> ExistsClientByPhoneAsync(long phone) => clientsDbContext.Clients.AnyAsync(x => x.Phone == phone);
    public Task<bool> ExistsClientByEmailAsync(string email) => clientsDbContext.Clients.AnyAsync(x => x.Email == email);
    public Task<bool> ExistsClientByUserNameAsync(string userName) => clientsDbContext.Clients.AnyAsync(x => x.UserName == userName);

    public async Task<Result<Client>> CreateAsync(long phone, string email, string userName,
        string firstName, string lastName, string patronymic)
    {
        var client = new Client
        {
            Phone = phone,
            Email = email,
            UserName = userName,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic
        };

        clientsDbContext.Clients.Add(client);

        await clientsDbContext.SaveChangesAsync();

        return client;
    }


    public async Task<Result<Client>> UpdatePhoneAsync(long clientId, long phone)
    {
        var client = await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client == null)
        {
            return Results.NotFoundById<Client>(clientId);
        }

        client.Phone = phone;

        await clientsDbContext.SaveChangesAsync();

        return client;
    }

    public async Task<Result<Client>> UpdateEmailAsync(long clientId, string email)
    {
        var client = await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client == null)
        {
            return Results.NotFoundById<Client>(clientId);
        }

        client.Email = email;

        await clientsDbContext.SaveChangesAsync();

        return client;
    }

    public async Task<Result<Client>> UpdateNamesAsync(long clientId, string firstName, string lastName, string patronymic)
    {
        var client = await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client == null)
        {
            return Results.NotFoundById<Client>(clientId);
        }

        client.FirstName = firstName;
        client.LastName = lastName;
        client.Patronymic = patronymic;

        await clientsDbContext.SaveChangesAsync();

        return client;
    }

    public async Task<Result<Client>> UpdatePasswordAsync(long clientId, Password password)
    {
        var client = await clientsDbContext.Clients.FirstOrDefaultAsync(x => x.ClientId == clientId);
        if (client == null)
        {
            return Results.NotFoundById<Client>(clientId);
        }

        client.Password = password;

        await clientsDbContext.SaveChangesAsync();

        return client;
    }


}
