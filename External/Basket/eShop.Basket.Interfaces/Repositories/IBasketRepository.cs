using eShop.Basket.Domain;

namespace eShop.Basket.Interfaces.Repositories;

public interface IBasketRepository
{
    Task<Result<BasketPosition>> Get(long clientId, int productId);

    Task<IReadOnlyCollection<BasketPosition>> GetAll(long clientId);


    Task<Result<BasketPosition>> Add(long clientId, int productId, string productName, double price, int quantity);

    Task<Result<BasketPosition>> UpdateQuantity(long clientId, int productId, int quantity);

    Task<Result<BasketPosition>> Remove(long clientId, int productId);

}
