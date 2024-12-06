using Server.Enums;

namespace Server.ViewModels.Response
{
    public class PersonDto
    {
        public int Key { get; set; }
        public string FullNames => $"{Names} {FirtSurname} {LastSurname}";
        public string Description { get; set; }
        public required string Names { get; set; }
        public required string FirtSurname { get; set; }
        public required string LastSurname { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Identification { get; set; }
        public DeathDto Death { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
    }
}
