using FreshVeggies.Api.Domain.Models;
using FreshVeggies.Api.Persistence;
using FreshVeggies.Api.Services.Abstracts;
using FreshVeggies.Shared.Dtos;
using FreshVeggies.Shared.Dtos.AddressDtos;
using FreshVeggies.Shared.Dtos.OrderDtos;
using Microsoft.EntityFrameworkCore;

namespace FreshVeggies.Api.Services
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ApiResult> ProcessOrderAsync(CreateOrderDto orderDto, int userId)
        {
            if (orderDto.Items.Length == 0)
            {
                return ApiResult.Failure("Order must contain at least one item");
            }
            // get product items by Ids and store in a dictionary
            var productIds = orderDto.Items.Select(p => p.ProductId).ToHashSet();

            var products = await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            if (products.Count == orderDto.Items.Length)
            {
                return ApiResult.Failure("Some products are not available");
            }

            // Create order items/line
            var orderItems = orderDto.Items.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                ProductImageUrl = products[x.ProductId].ImageUrl,
                ProductName = products[x.ProductId].Name,
                ProductPrice = products[x.ProductId].Price,
                Unit = products[x.ProductId].Unit
            }).ToArray();

            // Create order with items and address
            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                UserId = userId,
                UserAddressId = orderDto.UserAddressId,
                AddressName = orderDto.AddressName,
                FullAddress = orderDto.FullAddress,
                TotalItems = orderDto.Items.Length,
                TotalAmount = orderItems.Sum(oi => oi.Quantity * oi.ProductPrice),
                Items = orderItems
            };

            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                return ApiResult.Failure("An error occured while processing an order.");
            }
        }

        public async Task<OrderDto[]> GetUserOrdersAsync(int userId, int startIndex, int pageSize)
        {
            var addresses = await _context.Orders
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Id)
                .Skip(startIndex)
                .Take(pageSize)
                .Select(x => new OrderDto
                {
                    AddressName = x.AddressName,
                    FullAddress = x.FullAddress,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    TotalAmount = x.TotalAmount,
                    TotalItems = x.TotalItems,
                    Remarks = x.Remarks,
                    Status = x.Status,
                }).ToArrayAsync();

            return addresses;

        }

        public async Task<ApiResult<OrderItemDto[]>> GetUserOrderItemsAsync(int orderId, int userId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (order is null)
            {
                return ApiResult<OrderItemDto[]>.Failure("Order not found");
            }

            if (order.UserId != userId)
            {
                return ApiResult<OrderItemDto[]>.Failure("Order not found");
            }

            var items = order.Items.Select(x => new OrderItemDto
            {
                Id = x.Id,
                ProductId = x.ProductId,
                ProductImageUrl = x.ProductImageUrl,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                Quantity = x.Quantity,
                Unit = x.Unit,
            }).ToArray();

            return ApiResult<OrderItemDto[]>.Success(items);

        }
    }
}
