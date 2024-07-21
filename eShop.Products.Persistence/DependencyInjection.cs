using eShop.Products.Persistence.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Products.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPeristance(this IServiceCollection services)
    {


        return services.AddPeristance(); 
    }


}
