using Microsoft.EntityFrameworkCore;
using Server.Enums;
using Server.Services.Implementation;
using Server.ViewModels.Request;
using Test.Integration;

namespace Server.Test.Integration.Services
{
    public class PersonServiceTest : IntegrationTestBase
    {
        private readonly PersonService _personService;

        public PersonServiceTest()
            : base()
        {
            _personService = new PersonService(new UnitOfWork.Implementation.UnitOfWork(_context), _mapper);
        }

        [Fact]
        public async Task AddPerson_ShouldAddPersonCorrectly()
        {
            // Arrange: Preparar los datos de prueba
            var personRequest = new PersonRequest
            {
                Names = "John",
                FirstSurname = "Doe",
                LastSurname = "Smith",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateOnly(1990, 1, 1),
                Identification = "1234567890",
                CityId = 1,
                VillageId = 1,
                Description = "Some description",
                Death = null
            };

            // Act: Llamar al método Add en el servicio
            var result = await _personService.CreatePersonAsync(personRequest);

            // Assert: Verificar que la persona fue agregada correctamente
            Assert.NotNull(result);
            Assert.Equal("John", result.Names);
            Assert.Equal("Doe", result.FirtSurname);
            Assert.Equal("Smith", result.LastSurname);
            // Y así sucesivamente...
        }

        [Fact]
        public async Task AddPerson_WithDeath_ShouldAddPersonAndDeathCorrectly()
        {
            // Arrange: Preparar los datos de prueba
            var personRequest = new PersonRequest
            {
                Names = "John",
                FirstSurname = "doe",
                LastSurname = "smith arcode",
                Gender = GenderEnum.Male,
                DateOfBirth = new DateOnly(1990, 1, 1),
                Identification = "1234567890",
                CityId = 1,
                VillageId = 1,
                Description = "Some description",
                Death = new DeathRequest
                {
                    DateOfDeath = new DateOnly(2024, 1, 1),
                    CauseOfDeath = "Unknown",
                    LocationOfDeath = "Somewhere",
                    CityId = 1,
                    VillageId = 1
                }
            };

            var result = await _personService.CreatePersonAsync(personRequest);

            Assert.NotNull(result);
            Assert.Equal("John", result.Names);
            Assert.Equal("Doe", result.FirtSurname);
            Assert.Equal("Smith", result.LastSurname);
            Assert.NotNull(result.Death);
            Assert.Equal(new DateOnly(2024, 1, 1), result.Death.DateOfDeath);
            Assert.Equal("Unknown", result.Death.CauseOfDeath);
            Assert.Equal("Somewhere", result.Death.LocationOfDeath);

            var person = await _context.Persons
                .Include(p => p.Death)
                .FirstOrDefaultAsync(p => p.Identification == "1234567890");

            Assert.NotNull(person);
            Assert.NotNull(person.Death);
            Assert.Equal(new DateOnly(2024, 1, 1), person.Death.DateOfDeath);
            Assert.Equal("Unknown", person.Death.CauseOfDeath);
            Assert.Equal("Somewhere", person.Death.LocationOfDeath);
        }

        [Fact]
        public async Task GetPersonByFullName_ShouldReturnPersonCorrectly()
        {
            // Arrange: Preparar los datos de prueba

            var fullName = "John Doe Smith";

            var result = await _personService.GetPersonByFilterAsync(fullName);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnPersonCorrectly()
        {
            // Arrange: Preparar los datos de prueba

            var result = await _personService.GetFullInformationByIdAsync(1);
            Assert.NotNull(result);
        }
    }
}
