namespace Server.ViewModels.Response.Location
{
    public class CountryDto : LocationBaseDto
    {
        public IEnumerable<DepartmentsDTO> Departments { get; set; }
    }
}
