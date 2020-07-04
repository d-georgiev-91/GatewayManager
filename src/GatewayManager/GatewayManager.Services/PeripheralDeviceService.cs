using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public class PeripheralDeviceService : IPeripheralDeviceService
    {
        private readonly IDataService<PeripheralDevice> _peripheralDeviceDataService;

        public PeripheralDeviceService(IDataService<PeripheralDevice> peripheralDeviceDataService) => _peripheralDeviceDataService = peripheralDeviceDataService;

        public async Task AddAsync(PeripheralDevice peripheralDevice)
        {
            await _peripheralDeviceDataService.AddAsync(peripheralDevice);
            await _peripheralDeviceDataService.SaveChangesAsync();
        }

        public async Task<ServiceResult<PeripheralDevice>> GetByIdAsync(long peripheralDeviceId)
        {
            var serviceResult = new ServiceResult<PeripheralDevice>();

            var peripheralDevice = await _peripheralDeviceDataService.GetByIdAsync(peripheralDeviceId);

            if (peripheralDevice == null)
            {
                serviceResult.AddError(ErrorType.NotFound,
                    $"Peripheral device with UID {peripheralDeviceId} does not exists");

                return serviceResult;
            }

            serviceResult.Data = peripheralDevice;

            return serviceResult;
        }
    }
}