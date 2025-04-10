using FreshVeggies.Shared.Dtos.ProductDtos;

namespace FreshVeggies.Api.Services.Abstracts
{
    public interface IProductService
    {
        Task<ProductDto[]> GetProductAsync();
    }
}