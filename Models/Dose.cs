using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepAidTrackerApi.Models
{
    public class Dose
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int SupplementId { get; set; }
        [Required]
        public int SleepId { get; set; }
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public double DoseAmount { get; set; }
        [Required]
        public DateTime DoseDate { get; set; }

        public virtual Sleep Sleep { get; set; } = null!;
        public virtual Supplement Supplement { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
    }
}
