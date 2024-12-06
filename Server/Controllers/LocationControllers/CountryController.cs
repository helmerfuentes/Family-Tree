using Microsoft.AspNetCore.Mvc;
using Server.Services.Location;

namespace Server.Controllers.LocationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCountriesAsync()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetAllCountriesWithDepartmentsAsync()
        {
            var countries = await _countryService.GetAllCountriesWithDepartmentsAsync();
            return Ok(countries);
        }
    }
}
