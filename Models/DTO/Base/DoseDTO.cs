using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Base
{
    public class DoseDTO
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        public int SupplementId { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public double DoseAmount { get; set; }

        [Required]
        public DateTime DoseDate { get; set; }
    }
}
