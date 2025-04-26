using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Base
{
    public class SupplementDTO
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Unit { get; set; }
    }
}
