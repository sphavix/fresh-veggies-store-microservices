using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Shared.Dtos.AuthDtos
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        
        public string? Cellphone { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
