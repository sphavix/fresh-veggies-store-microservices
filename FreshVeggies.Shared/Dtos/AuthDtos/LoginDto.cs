using System.ComponentModel.DataAnnotations;

namespace FreshVeggies.Shared.Dtos.AuthDtos
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
