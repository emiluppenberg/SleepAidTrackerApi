using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Action
{
    public class UpdateSleepHoursDTO
    {
        [Required]
        public int SleepId { get; set; }
        [Required]
        public double NewHoursOfSleep { get; set; }
    }
}
