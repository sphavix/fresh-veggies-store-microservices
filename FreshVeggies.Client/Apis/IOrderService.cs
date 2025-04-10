using FreshVeggies.Shared.Dtos.OrderDtos;
using FreshVeggies.Shared.Dtos;
using Refit;

namespace FreshVeggies.Client.Apis
{
    [Headers("Authorization: Bearer ")]
    public interface IOrderService
    {
        [Post("/api/orders/createorder")]
        Task<ApiResult> ProcessOrderAsync(CreateOrderDto orderDto);

        [Get("/api/orders/user/{userId}/orders/{orderId}/items")]
        Task<ApiResult<OrderItemDto[]>> GetUserOrderItemsAsync(int orderId, int userId);

        [Get("/api/orders/user/{userId}")]
        Task<OrderDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize);
    }
}
