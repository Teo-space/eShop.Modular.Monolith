namespace eShop.Clients.Interfaces.ServicesExternal;

public interface ISmsSender
{
    Task SendAsync(long phone, string text);
}
