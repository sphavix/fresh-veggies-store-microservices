using FreshVeggies.Shared.Dtos.ProductDtos;
using Refit;

namespace FreshVeggies.Client.Apis
{
    public interface IProductService
    {
        [Get("/api/products")]
        Task<ProductDto[]> GetProductsAsync();
    }
}
