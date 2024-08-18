using eShop.Basket.Domain;
using eShop.Basket.Models;

namespace eShop.Basket.Interfaces.Services;

public interface IBasketService
{
    Task<Result<BasketPositionModel>> Get(long clientId, int productId);

    Task<IReadOnlyCollection<BasketPositionModel>> GetAll(long clientId);


    Task<Result<BasketPositionModel>> Add(long clientId, int productId, int quantity);

    Task<Result<BasketPositionModel>> UpdateQuantity(long clientId, int productId, int quantity);

    Task<Result<BasketPositionModel>> Remove(long clientId, int productId);
}


//Проверка цены и наличия
//Сервис расценки