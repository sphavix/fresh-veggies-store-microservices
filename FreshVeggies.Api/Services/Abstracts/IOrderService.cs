using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.OrderDtos;

namespace FreshVeggies.Api.Services.Abstracts
{
    public interface IOrderService
    {
        Task<ApiResult<OrderItemDto[]>> GetUserOrderItems(int orderId, int userId);
        Task<AddressDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize);
        Task<ApiResult> ProcessOrderAsync(CreateOrderDto orderDto, int userId);
    }
}