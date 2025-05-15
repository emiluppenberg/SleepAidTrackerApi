using SleepAidTrackerApi.Models.DTO.Base;
using System.ComponentModel.DataAnnotations;

namespace SleepAidTrackerApi.Models.DTO.Action
{
    public class SleepDosesDTO
    {
        [Required]
        public SleepDTO Sleep { get; set; } = new();

        public ICollection<DoseDTO> Doses { get; set; } = new List<DoseDTO>();
    }
}
