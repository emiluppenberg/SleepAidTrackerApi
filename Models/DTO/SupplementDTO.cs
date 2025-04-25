using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class SupplementDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Unit { get; set; } = null!;
    }
}
