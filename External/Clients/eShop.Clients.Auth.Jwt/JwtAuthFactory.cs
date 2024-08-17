using eShop.Clients.Auth.Jwt.Implementations;
using eShop.Clients.Auth.Jwt.Interfaces;

namespace eShop.Clients.Auth.Jwt;

public static class JwtAuthFactory
{
    public static IClient GetTestUser() => new TestClientAccessor();
}
