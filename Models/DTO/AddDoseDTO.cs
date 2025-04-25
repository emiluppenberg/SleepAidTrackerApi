using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class AddDoseDTO
    {
        [Required]
        public string UserId { get; set; } = null!;
        [Required]
        public int SleepId { get; set; }
        [Required] 
        public int SupplementId { get; set; }

        [Required]
        public double DoseAmount { get; set; }
        [Required]
        public DateTime DoseDate { get; set; }
    }
}
