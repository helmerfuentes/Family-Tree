using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.ViewModels.Request;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationshipController : ControllerBase
    {
        private readonly IRelationshipService _relationshipService;

        public RelationshipController(IRelationshipService relationshipService) => _relationshipService = relationshipService;

        [HttpPost("RegisterChild")]
        public async Task<IActionResult> RegisterRelationship([FromBody] RelationshipRequest relationship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _relationshipService.CreateRelationshipAsync(relationship);
            return Ok();
        }

        [HttpGet("GetRelationship/{id}/{padres}")]
        public async Task<IActionResult> GetRelationship(int id, bool padres)
        {
            var relationship = await _relationshipService.GetRelationshipByPersonIdAsync(id, padres);
            return Ok(relationship);
        }
    }
}
