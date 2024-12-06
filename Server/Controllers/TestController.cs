using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IServiceTest _serviceTest;

        public TestController(IServiceTest serviceTest)
        {
            _serviceTest = serviceTest;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = _serviceTest.HttpGetTest() });
        }
    }
}
