using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProtectedController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("This is a protected endpoint.");
        }
    }
}
