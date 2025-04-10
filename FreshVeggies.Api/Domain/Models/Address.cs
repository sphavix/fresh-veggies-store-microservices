using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Api.Domain.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required, MaxLength(255)]
        public string FullAddress { get; set; }
        [Required, MaxLength(255)]
        public string AddressName { get; set; }
        public bool IsDefault { get; set; }
    }
}
