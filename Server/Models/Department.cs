namespace Server.Models
{
    public class Department : BaseLocation
    {
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}
