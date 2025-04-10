using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Shared.Dtos.OrderDtos
{
    public class CreateOrderItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
