using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO
{
    public class UpdateDoseDTO
    {
        [Required]
        public int DoseId { get; set; }
        [Required]
        public int NewSupplementId { get; set; }


        [Required]
        public double NewDoseAmount { get; set; }
    }
}
