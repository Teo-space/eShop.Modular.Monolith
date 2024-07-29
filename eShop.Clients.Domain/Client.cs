namespace eShop.Clients.Domain;

public sealed class Client
{
    public long ClientId { get; set; }
    public long Phone { get; set; }
    public string Email { get; set; }

    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }


    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }

    public List<ClientToken> Tokens { get; set; } = new List<ClientToken>();
}
