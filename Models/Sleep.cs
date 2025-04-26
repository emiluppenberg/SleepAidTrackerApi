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
        public double HoursOfSleep { get; set; }
        [Required]
        public DateTime SleepDate { get; set; }
        public double? MinutesOfSleepDisruption { get; set; }

        public virtual IdentityUser User { get; set; } = null!;
        public virtual ICollection<Dose> Doses { get; set; } = null!;
    }
}
