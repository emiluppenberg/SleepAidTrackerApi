using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Base
{
    public class SleepDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public double TotalHours { get; set; }
        [Required]
        public DateTime SleepDate { get; set; }
        [Required]
        public TimeSpan Bedtime { get; set; }
        [Required]
        public TimeSpan Waketime { get; set; }

        public double? BedtimeHR { get; set; }
        public string? Note { get; set; }
        public double? DisruptionCount { get; set; }
    }
}
