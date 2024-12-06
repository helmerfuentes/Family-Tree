using Microsoft.AspNetCore.Mvc;
using Server.Services.Location;

namespace Server.Controllers.LocationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService departmentService) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;

        [HttpGet("byCountry/{countryId}")]
        public async Task<IActionResult> GetDepartmentsByCountryIdAsync(int countryId)
        {
            var departments = await _departmentService.GetDepartmentsByCountryIdAsync(countryId);
            return Ok(departments);
        }
    }
}
