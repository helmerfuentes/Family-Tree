namespace Server.Models
{
    public class City : BaseLocation
    {
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Village> Villages { get; set; }
        public ICollection<Person> Persons { get; set; }
        public ICollection<Death> Deaths { get; set; }
    }
}
