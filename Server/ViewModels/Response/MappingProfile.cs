using AutoMapper;
using Server.Models;
using Server.ViewModels.Response.Location;

namespace Server.ViewModels.Response
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapea el modelo de dominio Country a un DTO CountryDto
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src => src.Departments));

            CreateMap<Department, DepartmentsDTO>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(sourceMember => sourceMember.Id));

            CreateMap<City, CityDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(sourceMember => sourceMember.Id));

            CreateMap<Village, VillageDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(sourceMember => sourceMember.Id));

            CreateMap<Person, PersonDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Village, opt => opt.MapFrom(src => src.Village.Name))
                .ForMember(dest => dest.Death, opt => opt.MapFrom(src => src.Death));

            CreateMap<Death, DeathDto>()
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Village, opt => opt.MapFrom(src => src.Village.Name));
            // Agrega otros mapeos según sea necesario
        }
    }
}
