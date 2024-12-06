using Microsoft.AspNetCore.Mvc;
using Server.Services.Location;

namespace Server.Controllers.LocationControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillageController(IVillageService villageService) : ControllerBase
    {
        private readonly IVillageService _villageService = villageService;

        [HttpGet("ByCity/{cityId}")]
        public async Task<IActionResult> GetVillagesByCityAsync(int cityId)
        {
            var villages = await _villageService.GetVillagesByCityIdAsync(cityId);
            return Ok(villages);
        }
    }
}
