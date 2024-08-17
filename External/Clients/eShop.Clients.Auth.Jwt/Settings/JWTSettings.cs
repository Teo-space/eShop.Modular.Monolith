namespace eShop.Clients.Auth.Jwt.Settings;

public sealed record JWTSettings
{
    public string Secret { get; init; }
    public int ExpirationInMinutes { get; init; }
}
