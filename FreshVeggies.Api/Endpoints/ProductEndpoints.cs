using FreshVeggies.Api.Services.Abstracts;

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
            }).WithName("Products");

            return app;
        }
    }
}
