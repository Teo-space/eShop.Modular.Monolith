using NUlid;

namespace eShop.Clients.Domain.Models;

public sealed class RefreshToken
{
    public Ulid RefreshTokenId { get; set; }

    public long ClientId { get; set; }

    public bool IsUsed { get; set; }

    public DateTime Created { get; set; }
}
