using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class UpdateSleepDateDTO
    {
        [Required]
        public int SleepId { get; set; }
        [Required]
        public DateTime NewDate { get; set; }
    }
}
