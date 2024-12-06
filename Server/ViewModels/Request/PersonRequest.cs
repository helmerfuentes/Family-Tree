using Server.Enums;

namespace Server.ViewModels.Request
{
    public class PersonRequest
    {
        public required string Names { get; set; }
        public required string Description { get; set; }
        public required string FirstSurname { get; set; }
        public required string LastSurname { get; set; }
        public int CityId { get; set; }
        public bool IsDeceased { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Identification { get; set; }
        public DeathRequest Death { get; set; }
        public int VillageId { get; set; }
    }
}
