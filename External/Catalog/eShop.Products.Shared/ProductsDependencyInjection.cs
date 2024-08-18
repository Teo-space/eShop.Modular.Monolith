using eShop.Products.Shared.Services;
using eShop.Products.Persistence;
using eShop.Shared.Interfaces.SharedServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Products.Shared;

public static class ProductsDependencyInjection
{
    public static void AddProductsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistance();
        services.AddScoped<IProductsSharedService, ProductsSharedService>();

    }
}
