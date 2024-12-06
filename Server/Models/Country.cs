namespace Server.Models
{
    public class Country : BaseLocation
    {
        public IEnumerable<Department> Departments { get; set; }
    }
}
