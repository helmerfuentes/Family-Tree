using Server.ViewModels.Request;
using Server.ViewModels.Response;

namespace Server.Services
{
    public interface IPersonService
    {
        Task<PersonDto> CreatePersonAsync(PersonRequest personRequest);
        Task<IEnumerable<PersonDto>> GetPersonByFilterTypeAsync(string filter, string filterValue);
        Task<PersonDto> GetFullInformationByIdAsync(int id);
    }
}
