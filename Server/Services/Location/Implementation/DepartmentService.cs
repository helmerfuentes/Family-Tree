using AutoMapper;
using Server.UnitOfWork;
using Server.ViewModels.Response.Location;

namespace Server.Services.Location.Implementation
{
    public class DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) : IDepartmentService
    {
        public async Task<IEnumerable<DepartmentsDTO>> GetDepartmentsByCountryIdAsync(int countryId)
        {
            var departments = await unitOfWork.Departments.FindAsync(d => d.CountryId == countryId);
            var departmentsDto = mapper.Map<IEnumerable<DepartmentsDTO>>(departments);

            return departmentsDto;
        }
    }
}
