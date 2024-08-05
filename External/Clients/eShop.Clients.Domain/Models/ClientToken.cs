using NUlid;

namespace eShop.Clients.Domain.Models;

public sealed class ClientToken
{
    public long ClientId { get; set; }
    public Ulid TokenId { get; set; }
    public int TokenType { get; set; }

    public string Value { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
