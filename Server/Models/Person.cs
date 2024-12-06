using Server.Enums;

namespace Server.Models
{
    public class Person : BaseModel
    {
        public string FullNames { get; set; }
        public string Description { get; set; }
        public required string Names { get; set; }
        public required string FirtSurname { get; set; }
        public required string LastSurname { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Identification { get; set; }
        public Death Death { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int? VillageId { get; set; }
        public Village Village { get; set; }
        public ICollection<Relationship> MaternalFigure { get; set; }
        public ICollection<Relationship> PaternalFigure { get; set; }
        public int? MotherId { get; set; } // Nullable, as not all persons may have this data
        public Person? Mother { get; set; }
        public int? FatherId { get; set; } // Nullable
        public Person? Father { get; set; }
    }
}