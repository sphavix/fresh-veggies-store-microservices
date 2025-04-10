using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Api.Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;
        [Required, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(10)]
        public string? Cellphone { get; set; }
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        public virtual ICollection<Address> UserAddress { get; set; }
    }
}
