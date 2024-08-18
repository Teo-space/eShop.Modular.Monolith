using eShop.Shared.Interfaces.Models.Products;

namespace eShop.Shared.Interfaces.SharedServices;

public interface IProductsSharedService
{
    Task<Result<ProductModel>> GetProduct(int productId);
}
