using NUlid;

namespace eShop.Clients.Interfaces.Models;

public class AuthModel
{
    public Ulid RefreshToken { get; set; }

    public string JwtToken { get; set; }
}
