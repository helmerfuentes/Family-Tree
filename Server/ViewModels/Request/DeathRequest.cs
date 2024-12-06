namespace Server.ViewModels.Request
{
    public class DeathRequest
    {
        public DateTime DateOfDeath { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? LocationOfDeath { get; set; }
        public int CityId { get; set; }
        public int VillageId { get; set; }
    }
}
