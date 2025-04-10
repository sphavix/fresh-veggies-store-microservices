using FreshVeggies.Api.Extensions;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.OrderDtos;
using System.Security.Claims;

namespace FreshVeggies.Api.Endpoints
{
    public static class OrderEndpoints
    {
        public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
        {
            var endpointGroup = app.MapGroup("/api/orders")
                .RequireAuthorization()
                .WithTags("Orders");

            endpointGroup.MapPost("/create-order", async (CreateOrderDto model, IOrderService _orderService, ClaimsPrincipal principal) =>
            {
                return Results.Ok(await _orderService.ProcessOrderAsync(model, principal.GetUserId()));
            })
            .Produces<ApiResult>()
            .WithName("CreateOrder");

            endpointGroup.MapGet("/user/{userId:int}", async (int userId, int startIdex, int pageSize, IOrderService _orderService, ClaimsPrincipal principal) =>
            {
                // check if user is authorized
                if (userId != principal.GetUserId())
                {
                    return Results.Unauthorized();
                }

                var orders = await _orderService.GetUserOrdersAsync(principal.GetUserId(), startIdex, pageSize);
                return Results.Ok(orders);
            })
            .Produces<ApiResult<OrderDto[]>>()
            .WithName("GetUserOrders");

            endpointGroup.MapGet("/user/{userId:int}/orders/{orderId:int}/items", async (int userId, int orderId, IOrderService _orderService, ClaimsPrincipal principal) =>
            {
                // check if user us authorized
                if (userId != principal.GetUserId())
                {
                    return Results.Unauthorized();
                }

                var orders = await _orderService.GetUserOrderItemsAsync(principal.GetUserId(), orderId);
                return Results.Ok(orders);
            })
            .Produces<ApiResult<OrderItemDto[]>>()
            .WithName("GetUserOrdersItems");


            return endpointGroup;
        }
    }
}
