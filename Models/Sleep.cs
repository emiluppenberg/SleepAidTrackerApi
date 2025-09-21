using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepAidTrackerApi.Models
{
    public class Sleep
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public double TotalHours { get; set; }
        [Required]
        public DateTime SleepDate { get; set; }
        [Required]
        public TimeSpan Bedtime { get; set; }
        [Required]
        public TimeSpan Waketime { get; set; }

        public double? BedtimeHR { get; set; }
        public double? DisruptionCount { get; set; }
        public string? Note { get; set; }

        public virtual IdentityUser User { get; set; } = null!;
        public virtual List<Dose> Doses { get; set; } = new();
    }
}
