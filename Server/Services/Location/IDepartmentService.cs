using Server.ViewModels.Response.Location;

namespace Server.Services.Location
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentsDTO>> GetDepartmentsByCountryIdAsync(int countryId);
    }
}
