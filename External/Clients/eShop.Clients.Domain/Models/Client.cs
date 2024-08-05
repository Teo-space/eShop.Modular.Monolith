namespace eShop.Clients.Domain.Models;

public sealed class Client
{
    public long ClientId { get; set; }

    public string UserName { get; set; }

    public long Phone { get; set; }
    public bool IsPhoneConfirmded { get; set; } = false;

    public string Email { get; set; }
    public bool IsEmailConfirmded { get; set; } = false;

    public Password Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public List<ClientToken> Tokens { get; set; } = new List<ClientToken>();
}
