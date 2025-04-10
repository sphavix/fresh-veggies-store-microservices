using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Shared.Dtos.AddressDtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
