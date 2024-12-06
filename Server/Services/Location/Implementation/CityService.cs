using AutoMapper;
using Server.UnitOfWork;
using Server.ViewModels.Response.Location;

namespace Server.Services.Location.Implementation
{
    public class CityService(IUnitOfWork unitOfWork, IMapper mapper) : ICityService
    {
        public async Task<IEnumerable<CityDto>> GetCitiesByDepartmentIdAsync(int departmentId)
        {
            var cities = await unitOfWork.Cities.FindAsync(c => c.DepartmentId == departmentId);
            var citiesDto = mapper.Map<IEnumerable<CityDto>>(cities);

            return citiesDto;
        }
    }
}
