namespace FreshVeggies.Shared.Dtos.OrderDtos
{
    public record CreateOrderDto(int UserAddressId, string AddressName, string FullAddress, CreateOrderItemDto[] Items);
}
