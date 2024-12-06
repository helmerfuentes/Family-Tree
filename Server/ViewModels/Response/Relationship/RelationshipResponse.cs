using Server.Enums;

namespace Server.ViewModels.Response.Relationship
{
    public class RelationshipResponse
    {
        public int Key { get; set; }
        public string FullNames { get; set; }
        public GenderEnum Gender { get; set; }
        public string Description { get; set; }
    }
}
