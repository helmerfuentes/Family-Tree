using Server.ViewModels.Response.Location;

namespace Server.Services.Location
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetCitiesByDepartmentIdAsync(int departmentId);
    }
}
