using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class AddSleepDTO
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public double HoursOfSleep { get; set; }

        [Required]
        public DateTime SleepDate { get; set; }

        public ICollection<AddSleepDoseDTO>? Doses { get; set; } = null;
    }
}
