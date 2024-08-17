using eShop.Clients.Auth.Jwt.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace eShop.Clients.Auth.Jwt.Implementations;

internal class ClientAccessor(IHttpContextAccessor httpContextAccessor) : IClient
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;


    public ClaimsPrincipal User => 
        _httpContextAccessor?.HttpContext?.User 
        ?? throw new UnauthorizedAccessException("HttpContext.User is null");

}
