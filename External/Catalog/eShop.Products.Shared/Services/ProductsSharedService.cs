using eShop.Products.Domain.Models;
using eShop.Products.Interfaces.Repositories;
using eShop.Shared.Interfaces.Models.Products;
using eShop.Shared.Interfaces.SharedServices;

namespace eShop.Products.Shared.Services;

internal class ProductsSharedService(ICatalogRepository catalogRepository) : IProductsSharedService
{
    public async Task<Result<ProductModel>> GetProduct(int productId)
    {
        var productResult = await catalogRepository.GetProduct(productId);
        if (!productResult.Success)
        {
            return productResult.MapTo<Product, ProductModel>();
        }

        var product = productResult.Value;
        return new ProductModel
        {
            ProductId = product.ProductId,
            ProductName = product.Name,
            ProductNumber = product.Number,
            ProductDescription = product.Description,

            ProductTypeId = product.ProductTypeId,
            ProductTypeName = product.ProductType.Name,

            MakerId = product.MakerId,
            MakerName = product.Maker.Name,

            Price = product.Price,
            Availability = product.Availability,
            SalesCount = product.SalesCount,
            Stars = product.Stars,
            StarsCount = product.StarsCount,
            ReviewsCount = product.ReviewsCount,
        };
    }

}
