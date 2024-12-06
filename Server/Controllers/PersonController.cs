using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.ViewModels.Request;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CreatePersonAsync([FromBody] PersonRequest personRequest)
        {
            var person = await _personService.CreatePersonAsync(personRequest);
            return Ok(person);
        }

        [HttpGet("GetPersonByFilter/{filter}/{filterValue}")]
        public async Task<IActionResult> GetPersonByFilterTypeAsync(string filter, string filterValue)
        {
            var person = await _personService.GetPersonByFilterTypeAsync(filter, filterValue);
            return Ok(person);
        }

        [HttpGet("GetFullInformationById/{id}")]
        public async Task<IActionResult> GetFullInformationByIdAsync(int id)
        {
            var person = await _personService.GetFullInformationByIdAsync(id);
            return Ok(person);
        }
    }
}
