using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using WebSiteManager.Services;

namespace GatewayManager.Services
{
    public class GatewayService : IGatewayService
    {
        private readonly IDataService<Gateway> _gatewayDataService;

        public GatewayService(IDataService<Gateway> gatewayDataService)
        {
            _gatewayDataService = gatewayDataService;
        }

        public async Task<ServiceResult> AddAsync(Gateway gateway)
        {
            var result = new ServiceResult();
            var existingGateway = await _gatewayDataService.GetByIdAsync(gateway.SerialNumber);
            
            if (existingGateway != null)
            {
                return result.AddError(ErrorType.InvalidInput, "Gateway with such serial number already exists");
            }

            await _gatewayDataService.AddAsync(gateway);
            await _gatewayDataService.SaveChangesAsync();

            return result;
        }
    }
}
