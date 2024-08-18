using eShop.Clients.Interfaces.DbContexts;
using eShop.Clients.Interfaces.Repositories;
using eShop.Clients.Persistence.EntityFramework.DbContexts;
using eShop.Clients.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Clients.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        //services.AddDbContext<ClientsDbContext>(options => options.UseSqlServer)
        services.AddScoped<IClientsDbContext, ClientsDbContext>();
        services.AddScoped<IClientRepository, ClientRepository>();









        return services;
    }


}
