using eShop.Products.Interfaces.Repositories;
using eShop.Products.Persistence.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Products.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        //services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer)

        //services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer)

        services.AddScoped<ICatalogRepository, CatalogRepository>();



        return services;
    }


}
