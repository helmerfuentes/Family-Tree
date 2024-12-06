using Server.Models;
using Server.ViewModels.Response.Location;

namespace Server.Services.Location
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<IEnumerable<CountryDto>> GetAllCountriesWithDepartmentsAsync();
    }
}
