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
                .ForMember(dest => dest.Doses, opt => opt.MapFrom(src => src.Doses))
                .ReverseMap();

            // Action
        }
    }
}
