using FreshVeggies.Api.Persistence;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos.ProductDtos;
using Microsoft.EntityFrameworkCore;

namespace FreshVeggies.Api.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;


        public async Task<ProductDto[]> GetProductsAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price,
                    Unit = p.Unit
                }).ToArrayAsync();
        }


    }
}
