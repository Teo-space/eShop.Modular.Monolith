namespace eShop.Shared.Interfaces.Models.Clients;

public sealed record ClientModel
{
    public long ClientId { get; set; }

    public string UserName { get; set; }

    public long Phone { get; set; }
    public bool IsPhoneConfirmded { get; set; } = false;

    public string Email { get; set; }
    public bool IsEmailConfirmded { get; set; } = false;

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
}
