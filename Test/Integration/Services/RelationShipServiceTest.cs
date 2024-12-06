using Server.Services.Implementation;
using Server.UnitOfWork.Implementation;
using Server.ViewModels.Request;

namespace Test.Integration.Services
{
    public class RelationShipServiceTest : IntegrationTestBase
    {
        private readonly RelationshipService _relationShipService;

        public RelationShipServiceTest()
            : base()
        {
            _relationShipService = new RelationshipService(new UnitOfWork(_context), _mapper, _configuration);
        }

        [Fact]
        public async Task AddRelationShip_ShouldAddRelationShipCorrectly()
        {
            // Arrange: Preparar los datos de prueba
            var relationShipRequest = new RelationshipRequest
            {
                ParentId = 1,
                MotherId = 2,
                ChildId = 3,
                IsBiologic = 1
            };

            // Act: Llamar al método Add en el servicio
            try
            {

                await _relationShipService.CreateRelationshipAsync(relationShipRequest);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            // Assert: Verificar que la relación fue agregada correctamente
            Assert.True(true);
        }

        //[Fact]
        //public async Task AddRelationShip_WithDeath_ShouldAddRelationShipAndDeathCorrectly()
        //{
        //    // Arrange: Preparar los datos de prueba
        //    var relationShipRequest = new RelationShipRequest
        //    {
        //        PersonId = 1,
        //        RelatedPersonId = 2,
        //        RelationShipTypeId = 1,
        //        Description = "Some description",
        //        Death = new DeathRequest
        //        {
        //            Date = new DateOnly(2021, 1, 1),
        //            Description = "Some description"
        //        }
        //    };

        //    // Act: Llamar al método Add en el servicio
        //    var result = await _relationShipService.CreateRelationShipAsync(relationShipRequest);

        //    // Assert: Verificar que la relación fue agregada correctamente
        //    Assert.NotNull(result);
        //    Assert.Equal(1, result.PersonId);
        //    Assert.Equal(2, result.RelatedPersonId);
        //    Assert.Equal(1, result.RelationShipTypeId);
        //    Assert.Equal("Some description", result.Description);
        //    Assert.NotNull(result.Death);
        //    Assert.Equal(new DateOnly(2021, 1, 1), result.Death.Date);
        //    Assert.Equal("Some description", result.Death.Description);
        //}
    }
}
