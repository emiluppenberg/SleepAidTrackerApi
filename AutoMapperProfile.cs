using AutoMapper;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO.Action;
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

            CreateMap<Sleep, SleepDTO>()
                .ReverseMap();

            // Action
        }
    }
}
