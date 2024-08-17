namespace eShop.Clients.Auth.Jwt.Settings;

public sealed class JWTSettings
{
    public string Secret { get; set; }
    public int ExpirationInMinutes { get; set; }
}
