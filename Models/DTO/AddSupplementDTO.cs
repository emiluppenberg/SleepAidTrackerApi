using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class AddSupplementDTO
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Unit { get; set; } = null!;
    }
}
