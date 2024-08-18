using eShop.Basket.Domain;

namespace eShop.Basket.Interfaces.Services;

public interface IBasketPositionService
{
    public Task<Result<BasketPosition>> Get(long ClientId, int productId);
    public Task<IReadOnlyCollection<BasketPosition>> GetAll(long ClientId);

    public Task Add(long ClientId, int productId, int quantity);
    public Task UpdateQuantity(long ClientId, int productId, int quantity);
    public Task Remove(long ClientId, int productId);
}
//Проверка цены и наличия
//Сервис расценки