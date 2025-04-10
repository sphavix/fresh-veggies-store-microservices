using FreshVeggies.Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Api.Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [MaxLength(255)]
        public string Remarks { get; set; }
        [Required, MaxLength(15)]
        public string Status { get; set; } = nameof(OrderStatus.Placed);
        public int UserAddressId { get; set; }
        public string AddressName { get; set; }
        public string FullAddress { get; set; }
        public int TotalItems { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
    }
}
