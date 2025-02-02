using eShop.Basket.Domain;
using eShop.Basket.Interfaces.Repositories;
using eShop.Basket.Interfaces.Services;
using eShop.Basket.Models;
using eShop.Shared.Interfaces.SharedServices;

namespace eShop.Basket.Services;

/*
TODO
Расценка
Количество
Промоакции
*/
internal class BasketService(
    IBasketRepository basketRepository,
    IClientsSharedService clientsSharedService,
    IProductsSharedService productsSharedService
)
    : IBasketService
{

    public async Task<Result<BasketPositionModel>> Get(long clientId, int productId)
    {
        var basketPositionResult = await basketRepository.Get(clientId, productId);
        if (!basketPositionResult.Success)
        {
            return basketPositionResult.MapTo<BasketPosition, BasketPositionModel>();
        }

        var basketPosition = basketPositionResult.Value;
        return new BasketPositionModel
        {
            ProductId = basketPosition.ProductId,
            ProductName = basketPosition.ProductName,
            Price = basketPosition.Price,
            Quantity = basketPosition.Quantity,
            Updated = basketPosition.Updated.Date.ToShortDateString(),
        };
    }

    public async Task<IReadOnlyCollection<BasketPositionModel>> GetAll(long clientId)
    {
        var basketPositions = await basketRepository.GetAll(clientId);

        return basketPositions.Select(basketPosition => new BasketPositionModel
        {
            ProductId = basketPosition.ProductId,
            ProductName = basketPosition.ProductName,
            Price = basketPosition.Price,
            Quantity = basketPosition.Quantity,
            Updated = basketPosition.Updated.Date.ToShortDateString(),
        }).ToArray();
    }

    public async Task<Result<BasketPositionModel>> Add(long clientId, int productId, int quantity)
    {
        if (await clientsSharedService.ExistsClientByIdAsync(clientId))
        {
            return Results.NotFound<BasketPositionModel>($"Клиент '{clientId}' не найден");
        }

        var productResult = await productsSharedService.GetProduct(productId);
        if (!productResult.Success)
        {
            return Results.NotFound<BasketPositionModel>($"Товар '{productId}' не найден");
        }
        if (productResult.Value.Availability < quantity)
        {
            return Results.Conflict<BasketPositionModel>(
                $"Количество '{quantity}' товара '{productId}' превышает доступное '{productResult.Value.Availability}'");
        }

        var product = productResult.Value;

        var basketPositionResult = await basketRepository.Add(clientId, product.ProductId, product.ProductName, product.Price, quantity);
        if (!basketPositionResult.Success)
        {
            return basketPositionResult.MapTo<BasketPosition, BasketPositionModel>();
        }

        var basketPosition = basketPositionResult.Value;
        return new BasketPositionModel
        {
            ProductId = basketPosition.ProductId,
            ProductName = basketPosition.ProductName,
            Price = basketPosition.Price,
            Quantity = basketPosition.Quantity,
            Updated = basketPosition.Updated.Date.ToShortDateString(),
        };
    }

    public async Task<Result<BasketPositionModel>> UpdateQuantity(long clientId, int productId, int quantity)
    {
        if (await clientsSharedService.ExistsClientByIdAsync(clientId))
        {
            return Results.NotFound<BasketPositionModel>($"Клиент '{clientId}' не найден");
        }

        var basketPositionResult = await basketRepository.Get(clientId, productId);
        if (!basketPositionResult.Success)
        {
            return basketPositionResult.MapTo<BasketPosition, BasketPositionModel>();
        }

        var productResult = await productsSharedService.GetProduct(productId);
        if (!productResult.Success)
        {
            return Results.NotFound<BasketPositionModel>($"Товар '{productId}' не найден");
        }
        if (productResult.Value.Availability < quantity)
        {
            return Results.Conflict<BasketPositionModel>(
                $"Количество '{quantity}' товара '{productId}' превышает доступное '{productResult.Value.Availability}'");
        }

        await basketRepository.UpdateQuantity(clientId, productId, quantity);

        var basketPosition = basketPositionResult.Value;
        return new BasketPositionModel
        {
            ProductId = basketPosition.ProductId,
            ProductName = basketPosition.ProductName,
            Price = basketPosition.Price,
            Quantity = basketPosition.Quantity,
            Updated = basketPosition.Updated.Date.ToShortDateString(),
        };
    }

    public async Task<Result<BasketPositionModel>> Remove(long clientId, int productId)
    {
        var removeResult = await basketRepository.Remove(clientId, productId);

        var basketPosition = removeResult.Value;
        return new BasketPositionModel
        {
            ProductId = basketPosition.ProductId,
            ProductName = basketPosition.ProductName,
            Price = basketPosition.Price,
            Quantity = basketPosition.Quantity,
            Updated = basketPosition.Updated.Date.ToShortDateString(),
        };
    }

}
