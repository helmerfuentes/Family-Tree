namespace Server.Models
{
    public class Village : BaseLocation
    {
        public int CityId { get; set; }
        public City City { get; set; }
        public ICollection<Person> Persons { get; set; }
        public ICollection<Death> Deaths { get; set; }
    }
}
