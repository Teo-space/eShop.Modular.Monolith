﻿using eShop.Clients.Auth.Jwt.Implementations;
using eShop.Clients.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace eShop.Clients.Auth.Jwt;

public static class JwtAuthDependencyInjection
{
    public static void AddUserAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IClient, ClientAccessor>();
    }

    public static void AddTestUserAccessor(this IServiceCollection services)
    {
        services.AddScoped<IClient, TestClientAccessor>();
    }

    public static void AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        var JWTSettings = configuration.GetSection("JWTSettings");
        var jwtSecret = JWTSettings.GetValue<string>("Secret")
            ?? throw new Exception("jwtSecret not found ");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false,
                 ValidateIssuer = false,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
             };
         });
    }

}