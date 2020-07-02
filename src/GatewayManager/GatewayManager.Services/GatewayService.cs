using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IDataService<Gateway> _gatewayDataService;

        public GatewayService(IDataService<Gateway> gatewayDataService)
        {
            _gatewayDataService = gatewayDataService;
        }

        public async Task Add(Gateway gateway) => await _gatewayDataService.Add(gateway);
    }
}
