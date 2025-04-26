using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Action
{
    public class UpdateSleepDateDTO
    {
        [Required]
        public int SleepId { get; set; }
        [Required]
        public DateTime NewDate { get; set; }
    }
}
