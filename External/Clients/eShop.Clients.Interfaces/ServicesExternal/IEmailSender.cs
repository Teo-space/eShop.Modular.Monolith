namespace eShop.Clients.Interfaces.ServicesExternal;

public interface IEmailSender
{
    Task SendAsync(string email, string title, string body);
}
