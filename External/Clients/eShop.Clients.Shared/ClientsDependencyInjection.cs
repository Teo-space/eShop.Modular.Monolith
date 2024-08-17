using eShop.Clients.Shared.Services;
using eShop.Shared.Interfaces.SharedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Clients.Shared;

public static class ClientsDependencyInjection
{
    public static void AddClientsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IClientsSharedService, ClientsSharedService>();

    }

}
