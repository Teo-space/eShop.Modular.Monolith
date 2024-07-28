using eShop.Products.Interfaces.DbContexts;
using eShop.Products.Interfaces.Repositories;
using eShop.Products.Persistence.EntityFramework.DbContexts;
using eShop.Products.Persistence.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Products.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPeristance(this IServiceCollection services)
    {
        //services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer)
        services.AddScoped<IProductsDbContext, ProductsDbContext>();

        //services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer)
        services.AddScoped<ICatalogDbContext, CatalogDbContext>();


        services.AddScoped<ICatalogRepository, CatalogRepository>();

        return services.AddPeristance(); 
    }


}
