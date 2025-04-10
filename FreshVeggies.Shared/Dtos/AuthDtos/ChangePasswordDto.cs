using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FreshVeggies.Shared.Dtos.AuthDtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrerntPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }

        [JsonIgnore]
        [Required, Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
}
