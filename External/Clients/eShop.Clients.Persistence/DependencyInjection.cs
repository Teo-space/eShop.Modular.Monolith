using eShop.Clients.Interfaces.Repositories;
using eShop.Clients.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Clients.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        //services.AddDbContext<ClientsDbContext>(options => options.UseSqlServer)

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();









        return services;
    }


}
