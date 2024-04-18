using AutoMapper;
using World.API.Dto.CountryDto;
using World.API.Models;

namespace World.API.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
        }
    }
}
