using AutoMapper;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO.Base;

namespace SleepAidTrackerApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Base
            CreateMap<Dose, DoseDTO>()
                .ReverseMap();

            CreateMap<Supplement, SupplementDTO>()
                .ReverseMap();

            CreateMap<SleepDTO, Sleep>()
                .ReverseMap();

            // Action
        }
    }
}
