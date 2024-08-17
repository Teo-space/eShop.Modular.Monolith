using eShop.Clients.Auth.Jwt.Interfaces;
using System.Security.Claims;

namespace eShop.Clients.Auth.Jwt.Implementations;

internal class TestClientAccessor : IClient
{
    public ClaimsPrincipal User
    {
        get
        {
            return new ClaimsPrincipal(new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new Claim []
                {
                    new Claim("UserName", "TestUser"),
                    new Claim("ClientId", 1321314521323123L.ToString()),
                    new Claim("Phone", 7_987_123_4567L.ToString()),

                    new Claim("Email", "test.user@gmail.com"),
                    
                })
            });
        }
    }
}
