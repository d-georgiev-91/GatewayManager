using AutoMapper;
using GatewayManager.Web.Tests.AutoMapper;

namespace GatewayManager.Web.Tests.Controllers
{
    public class ControllerTestsBase
    {
        protected IMapper CreateAndConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestsProfile>();
            });

            return config.CreateMapper();
        }
    }
}