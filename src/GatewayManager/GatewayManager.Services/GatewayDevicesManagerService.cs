using System.Threading.Tasks;

namespace GatewayManager.Services
{
    public class GatewayDevicesManagerService : IGatewayDevicesManagerService
    {
        private readonly IGatewayService _gatewayService;
        private readonly IPeripheralDeviceService _peripheralDeviceService;

        public GatewayDevicesManagerService(IGatewayService gatewayService, IPeripheralDeviceService peripheralDeviceService)
        {
            _gatewayService = gatewayService;
            _peripheralDeviceService = peripheralDeviceService;
        }

        public async Task<ServiceResult> AssignPeripheralDeviceAsync(string serialNumber, long peripheralDeviceId)
        {
            var peripheralDeviceServiceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            if (peripheralDeviceServiceResult.HasErrors)
            {
                return peripheralDeviceServiceResult;
            }

            var serviceResult = await _gatewayService.AddPeripheralDeviceAsync(serialNumber,
                peripheralDeviceServiceResult.Data);

            return serviceResult;
        }

        public async Task<ServiceResult> RemovePeripheralDeviceAsync(string serialNumber, long peripheralDeviceId)
        {
            var peripheralDeviceServiceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            if (peripheralDeviceServiceResult.HasErrors)
            {
                return peripheralDeviceServiceResult;
            }

            var serviceResult = await _gatewayService.RemovePeripheralDeviceAsync(serialNumber, peripheralDeviceServiceResult.Data);

            return serviceResult;
        }
    }
}