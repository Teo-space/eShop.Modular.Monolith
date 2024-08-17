using eShop.Clients.Auth.Jwt;
using eShop.Clients.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Clients.Services;

public static class ClientsDependencyInjection
{
    public static void AddClientsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtBearerSettings(configuration);
        services.AddJwtBearer(configuration);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IClientService, ClientService>();


    }

}
