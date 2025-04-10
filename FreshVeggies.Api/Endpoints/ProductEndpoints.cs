using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos.ProductDtos;

namespace FreshVeggies.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/products", async (IProductService productService) =>
            {
                var products = await productService.GetProductsAsync();
                return Results.Ok(products);
            })
            .Produces<ProductDto[]>()
            .WithName("Products");

            return app;
        }
    }
}
