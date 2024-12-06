using AutoMapper;
using Server.UnitOfWork;
using Server.ViewModels.Response.Location;

namespace Server.Services.Location.Implementation
{
    public class VillageService(IUnitOfWork unitOfWork, IMapper mapper) : IVillageService
    {
        public async Task<IEnumerable<VillageDto>> GetVillagesByCityIdAsync(int cityId)
        {
            var villages = await unitOfWork.Villages.FindAsync(v => v.CityId == cityId);
            var villagesDto = mapper.Map<IEnumerable<VillageDto>>(villages);

            return villagesDto;
        }
    }
}
