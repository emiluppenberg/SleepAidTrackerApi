using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Auth
{
    public class LoginResultDTO
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Token { get; set; } = null!;
    }
}