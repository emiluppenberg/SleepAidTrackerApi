using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class UpdateSleepHoursDTO
    {
        [Required]
        public int SleepId { get; set; }
        [Required]
        public double NewHoursOfSleep { get; set; }
    }
}
