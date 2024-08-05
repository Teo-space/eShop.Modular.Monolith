namespace eShop.Clients.Domain.Models;

public sealed record Password
{
    public string Hash { get; init; }
    public string Salt { get; init; }
}
