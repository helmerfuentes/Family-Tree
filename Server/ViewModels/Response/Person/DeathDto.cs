namespace Server.ViewModels.Response
{
    public class DeathDto
    {
        public int Key { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string? CauseOfDeath { get; set; }
        public string? LocationOfDeath { get; set; }
        public string City { get; set; }
        public string Village { get; set; }
    }
}
