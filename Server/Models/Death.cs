namespace Server.Models
{
    public class Death
    {
        public int Id { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? LocationOfDeath { get; set; }
        public string? AdditionalInformation { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int? VillageId { get; set; }
        public Village Village { get; set; }
    }
}
