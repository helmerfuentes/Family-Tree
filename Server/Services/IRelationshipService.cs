using Server.ViewModels.Request;
using Server.ViewModels.Response.Relationship;

namespace Server.Services
{
    public interface IRelationshipService
    {
        Task CreateRelationshipAsync(RelationshipRequest relationshipRequest);
        Task<IEnumerable<RelationshipResponse>> GetRelationshipByPersonIdAsync(int id, bool parents);
    }
}
