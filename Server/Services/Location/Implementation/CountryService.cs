using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.UnitOfWork;
using Server.ViewModels.Response.Location;

namespace Server.Services.Location.Implementation
{
    public class CountryService(IUnitOfWork unitOfWork, IMapper mapper) : ICountryService
    {
        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await unitOfWork.Countries.GetAllAsync();
        }

        public async Task<IEnumerable<CountryDto>> GetAllCountriesWithDepartmentsAsync()
        {
            var countries = await unitOfWork.Countries.GetAll()
                .Include(c => c.Departments)
                .ToListAsync();
            var countriesDto = mapper.Map<IEnumerable<CountryDto>>(countries);

            return countriesDto;
        }
    }
}
