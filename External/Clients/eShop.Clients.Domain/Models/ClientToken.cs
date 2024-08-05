namespace eShop.Clients.Domain.Models;

public sealed class ClientToken
{
    public long ClientId { get; set; }
    public int TokenId { get; set; }

    public int TokenType { get; set; }
    public bool IsUsed { get; set; }

    public string Value { get; set; }

    public DateTime Created { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
}
