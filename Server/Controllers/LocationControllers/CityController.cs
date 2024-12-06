using Microsoft.AspNetCore.Mvc;
using Server.Services.Location;

namespace Server.Controllers.LocationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController(ICityService cityService) : ControllerBase
    {
        private readonly ICityService _cityService = cityService;

        [HttpGet("ByDepartment/{departmentId}")]
        public async Task<IActionResult> GetCitiesByDepartment(int departmentId)
        {
            var cities = await _cityService.GetCitiesByDepartmentIdAsync(departmentId);
            return Ok(cities);
        }
    }
}
