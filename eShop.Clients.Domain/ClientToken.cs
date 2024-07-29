using NUlid;

namespace eShop.Clients.Domain;

public sealed class ClientToken
{
    public long ClientId { get; set; }
    public Ulid TokenId { get; set; }

    public long? Phone {  get; set; }

    public string Email { get; set; }
}
