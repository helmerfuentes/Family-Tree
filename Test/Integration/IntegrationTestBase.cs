using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Data;
using Server.Enums;
using Server.Models;
using Server.ViewModels.Response;

namespace Test.Integration
{
    public class IntegrationTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly IConfiguration _configuration;

        public IntegrationTestBase()
        {
            var inMemorySettings = new Dictionary<string, string> {
            { "ConnectionStrings:DefaultConnection", "Data Source=FamililyThree.db" } // Aquí pones la ruta de tu base de datos real
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings) // Utiliza la configuración en memoria para las pruebas
                .Build();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseSqlite(_configuration.GetConnectionString("DefaultConnection"))  // Aquí usas la cadena de conexión real
           .Options;

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            //    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //    .Options;

            _context = new ApplicationDbContext(options);

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();

            //var inMemorySettings = new Dictionary<string, string> {
            //{ "ConnectionStrings:DefaultConnection", "DataSource=:memory:" } // Configuración para SQLite en memoria
            // };

            //_configuration = new ConfigurationBuilder()
            //    .AddInMemoryCollection(inMemorySettings)
            //    .Build();

            //InitializeData();
        }

        protected virtual void InitializeData()
        {
            var country = new Country { Name = "Colombia" };
            var department = new Department { Name = "Cesar", Country = country };
            var city = new City { Name = "Valledupar", Department = department };
            var village = new Village { Name = "Atanquez", City = city };
            var death = new Death { DateOfDeath = new DateTime(2020, 1, 1), CauseOfDeath = "Natural", LocationOfDeath = "Hospital", City = city, Village = village };
            IEnumerable<Person> people = new List<Person>
            {
                new Person
                {
                    FirtSurname = "madre",
                    LastSurname = "madre",
                    Description = "Some description",
                    Names = "madre",
                    FullNames = "madre Doe Smith",
                    City = city,
                    Village = village,
                    Gender = GenderEnum.Male,
                    Identification = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Death = death
                },
                new Person
                {
                    FirtSurname = "padre",
                    LastSurname = "padre",
                    Description = "Some description",
                    Names = "padre",
                    FullNames = "padre Doe Smith",
                    City = city,
                    Village = village,
                    Gender = GenderEnum.Male,
                    Identification = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Death = death
                },
                new Person
                {
                    FirtSurname = "Hijo",
                    LastSurname = "Smith",
                    Description = "Some description",
                    Names = "Hijo",
                    FullNames = "Hijo Doe Smith",
                    City = city,
                    Village = village,
                    Gender = GenderEnum.Male,
                    Identification = "1234567890",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Death = death
                }
            };

            //_context.Countries.Add(country);
            //_context.Departments.Add(department);
            //_context.Cities.Add(city);
            //_context.Villages.Add(village);
            _context.Persons.AddRange(people);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
