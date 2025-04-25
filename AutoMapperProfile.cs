using AutoMapper;
using SleepAidTrackerApi.Models;
using SleepAidTrackerApi.Models.DTO;

namespace SleepAidTrackerApi
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Supplement, SupplementDTO>()
                .ReverseMap();

            CreateMap<Dose, AddSleepDoseDTO>()
                .ReverseMap();

            CreateMap<AddSleepDTO, Sleep>()
                .ForMember(dest => dest.Doses, opt => opt.MapFrom(src => src.Doses));

            CreateMap<Dose, AddDoseDTO>()
                .ReverseMap();
        }
    }
}
