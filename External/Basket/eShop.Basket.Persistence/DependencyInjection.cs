﻿using eShop.Basket.Interfaces.Repositories;
using eShop.Basket.Persistence.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace eShop.Basket.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        //services.AddDbContext<>(options => options.UseSqlServer)

        services.AddScoped<IBasketRepository, BasketRepository>();



        return services;
    }

}
