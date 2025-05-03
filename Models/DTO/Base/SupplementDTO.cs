using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Base
{
    public class SupplementDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Unit { get; set; } = null!;

        public ICollection<DoseDTO>? Doses { get; set; }
    }
}
