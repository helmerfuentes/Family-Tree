using Server.ViewModels.Response.Location;

namespace Server.Services.Location
{
    public interface IVillageService
    {
        Task<IEnumerable<VillageDto>> GetVillagesByCityIdAsync(int cityId);
    }
}
