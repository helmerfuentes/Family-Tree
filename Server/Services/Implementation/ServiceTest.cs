using Server.UnitOfWork;

namespace Server.Services.Implementation
{
    public class ServiceTest: IServiceTest
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServiceTest(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string HttpGetTest()
        {
            return "test service";
        }
    }
}
