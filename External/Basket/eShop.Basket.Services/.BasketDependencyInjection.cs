using eShop.Basket.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Basket.Services;

public static class BasketDependencyInjection
{
    public static void AddBasketServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IBasketService, BasketService>();
    }
}
