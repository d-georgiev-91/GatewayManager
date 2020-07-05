using System.Threading.Tasks;

namespace GatewayManager.Services
{
    public class GatewayDeviceManager : IGatewayDeviceManager
    {
        private const int AllowedPeripheralDevicesPerGateway = 10;

        private readonly IGatewayService _gatewayService;
        private readonly IPeripheralDeviceService _peripheralDeviceService;

        public GatewayDeviceManager(IGatewayService gatewayService, IPeripheralDeviceService peripheralDeviceService)
        {
            _gatewayService = gatewayService;
            _peripheralDeviceService = peripheralDeviceService;
        }

        public async Task<ServiceResult> AssignPeripheralDevice(string serialNumber, long peripheralDeviceId)
        {
            var gatewayServiceResult = await _gatewayService.FindAsync(serialNumber);

            if (gatewayServiceResult.HasErrors)
            {
                return gatewayServiceResult;
            }

            var gateway = gatewayServiceResult.Data;

            var serviceResult = new ServiceResult();

            if (gateway.PeripheralDevices.Count >= AllowedPeripheralDevicesPerGateway)
            {
                serviceResult.AddError(ErrorType.InvalidInput,
                    $"Only {AllowedPeripheralDevicesPerGateway} peripheral devices are allowed per gateway");

                return serviceResult;
            }

            var peripheralDeviceServiceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            if (peripheralDeviceServiceResult.HasErrors)
            {
                return peripheralDeviceServiceResult;
            }

            var peripheralDevice = peripheralDeviceServiceResult.Data;

            await _gatewayService.AddPeripheralDeviceAsync(gateway, peripheralDevice);

            return serviceResult;
        }

        public async Task<ServiceResult> RemovePeripheralDeviceAsync(string serialNumber, long peripheralDeviceId)
        {
            var gatewayServiceResult = await _gatewayService.FindAsync(serialNumber);

            if (gatewayServiceResult.HasErrors)
            {
                return gatewayServiceResult;
            }

            var gateway = gatewayServiceResult.Data;

            var serviceResult = new ServiceResult();

            var peripheralDeviceServiceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            if (peripheralDeviceServiceResult.HasErrors)
            {
                return peripheralDeviceServiceResult;
            }

            var peripheralDevice = peripheralDeviceServiceResult.Data;

            await _gatewayService.RemovePeripheralDeviceAsync(gateway, peripheralDevice);

            return serviceResult;
        }
    }
}