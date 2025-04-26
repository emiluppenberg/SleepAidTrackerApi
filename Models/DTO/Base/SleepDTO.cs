using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Base
{
    public class SleepDTO
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public double? HoursOfSleep { get; set; }

        [Required]
        public DateTime? SleepDate { get; set; }

        public double? MinutesOfSleepDisruption { get; set; }
        public ICollection<DoseDTO>? Doses { get; set; } = null;
    }
}
