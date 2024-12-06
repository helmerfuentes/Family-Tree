using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Server.Extensions;
using Server.Helper;
using Server.Models;
using Server.UnitOfWork;
using Server.ViewModels.Request;
using Server.ViewModels.Response;

namespace Server.Services.Implementation
{
    public class PersonService(IUnitOfWork unitOfWork, IMapper mapper) : IPersonService
    {
        public async Task<PersonDto> CreatePersonAsync(PersonRequest request)
        {
            ValidationHelper.ValidateObject(request, nameof(request));
            ValidationHelper.ValidateStringValue(request.Names, nameof(request.Names));
            ValidationHelper.ValidateStringValue(request.Description, nameof(request.Description));
            ValidationHelper.ValidateStringValue(request.FirstSurname, nameof(request.FirstSurname));
            ValidationHelper.ValidateIntValue(request.CityId, nameof(request.CityId));
            ValidationHelper.ValidateDateTimeValue(request.DateOfBirth, nameof(request.DateOfBirth));
            ValidationHelper.ValidateIntValue((int)request.Gender, nameof(request.Gender));


            var fullNames = $"{request.Names} {request.FirstSurname} {request.LastSurname}";

            var person = new Person
            {
                FullNames = fullNames.SanitizeNames(),
                Description = request.Description,
                Names = request.Names.SanitizeNames(),
                FirtSurname = request.FirstSurname.SanitizeNames(),
                LastSurname = request.LastSurname.SanitizeNames(),
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                Identification = request.Identification,
                CityId = request.CityId,
                VillageId = request.VillageId == 0 ? null : request.VillageId,
            };

            if (request.IsDeceased)
            {
                ValidationHelper.ValidateObject(request.Death, nameof(request.Death));
                ValidationHelper.ValidateIntValue(request.Death.CityId, nameof(request.Death.CityId));
                ValidationHelper.ValidateDateTimeValue(request.Death.DateOfDeath, nameof(request.Death.DateOfDeath));
                ValidationHelper.ValidateStringValue(request.Death.CauseOfDeath, nameof(request.Death.CauseOfDeath));
                ValidationHelper.ValidateStringValue(request.Death.LocationOfDeath, nameof(request.Death.LocationOfDeath));

                person.Death = new Death
                {
                    DateOfDeath = request.Death.DateOfDeath,
                    CauseOfDeath = request.Death.CauseOfDeath,
                    LocationOfDeath = request.Death.LocationOfDeath,
                    CityId = request.Death.CityId,
                    VillageId = request.Death.VillageId == 0 ? null : request.Death.VillageId
                };
            }

            try
            {
                unitOfWork.Persons.Add(person);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            var personDto = mapper.Map<PersonDto>(person);
            return personDto;
        }

        public async Task<IEnumerable<PersonDto>> GetPersonByFilterTypeAsync(string filter, string filterValue)
        {
            /*
              IMPROVE
          - Pagination 
          - implement porcentaje  the search
          */
            var personsResponse = Enumerable.Empty<PersonDto>();

            var sanitizedFullName = filterValue.SanitizeNames();

            IQueryable<Person> query = unitOfWork.Persons.GetAll()
                .AsNoTracking();
            query = filter.ToLower() switch
            {
                "fullnames" => query.Where(p => EF.Functions.Like(p.FullNames.ToLower(), $"%{sanitizedFullName}%")),
                "identification" => query.Where(p => EF.Functions.Like(p.Identification.ToLower(), $"%{sanitizedFullName}%")),
                "firtsurname" => query.Where(p => EF.Functions.Like(p.FirtSurname.ToLower(), $"%{sanitizedFullName}%")),
                "lastsurname" => query.Where(p => EF.Functions.Like(p.LastSurname.ToLower(), $"%{sanitizedFullName}%")),
                "names" => query.Where(p => EF.Functions.Like(p.Names.ToLower(), $"%{sanitizedFullName}%")),
                _ => throw new ArgumentException("Invalid filter specified")
            };

            if (query.HasValues())
            {
                personsResponse = mapper.Map<IEnumerable<PersonDto>>(query);
            }

            return personsResponse;
        }

        public async Task<PersonDto> GetFullInformationByIdAsync(int id)
        {
            PersonDto? personsResponse = null;
            var persons = await unitOfWork.Persons.FindAsync(p => p.Id == id,
               query =>
               {
                   IQueryable<Person> personDtos = query
                                           .AsNoTracking()
                                           .Include(p => p.City)
                                           .Include(p => p.Village)
                                           .Include(p => p.Death)
                                           .Select(p => new Person
                                           {
                                               Id = p.Id,
                                               Description = p.Description,
                                               Names = p.Names,
                                               FirtSurname = p.FirtSurname,
                                               LastSurname = p.LastSurname,
                                               City = new City
                                               {
                                                   Name = p.City.Name
                                               },

                                               Village = new Village
                                               {
                                                   Name = p.Village.Name
                                               },
                                               DateOfBirth = p.DateOfBirth,
                                               Gender = p.Gender,
                                               Identification = p.Identification,
                                               Death = new Death
                                               {
                                                   DateOfDeath = p.Death.DateOfDeath,
                                                   CauseOfDeath = p.Death.CauseOfDeath,
                                                   LocationOfDeath = p.Death.LocationOfDeath,
                                                   City = new City
                                                   {
                                                       Name = p.Death.City.Name
                                                   },
                                                   Village = new Village
                                                   {
                                                       Name = p.Death.Village.Name
                                                   }
                                               }
                                           });
                   return personDtos;
               });

            if (persons.HasValues())
            {
                personsResponse = mapper.Map<PersonDto>(persons.First());
            }

            return personsResponse!;
        }
    }
}
