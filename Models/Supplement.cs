using Microsoft.AspNetCore.Identity;
using SleepAidTrackerApi.Models.DTO.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepAidTrackerApi.Models
{
    public class Supplement
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        [Required]
        public virtual IdentityUser User { get; set; } = null!;
        public virtual ICollection<Dose>? Doses { get; set; }
    }
}
