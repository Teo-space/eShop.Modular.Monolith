namespace eShop.Basket.Interfaces.Services;

public interface IBasketPositionService
{
    public Task Get(long ClientId, int productId);
    public Task Get(long ClientId);

    public Task Add(long ClientId, int productId, int quantity);
    public Task UpdateQuantity(long ClientId, int productId, int quantity);
    public Task Remove(long ClientId, int productId, int quantity);

}
//Проверка цены и наличия
//Сервис расценки