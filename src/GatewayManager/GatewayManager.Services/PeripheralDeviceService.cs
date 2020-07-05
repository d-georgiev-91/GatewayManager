using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public class PeripheralDeviceService : IPeripheralDeviceService
    {
        private const string PeripheralDeviceNotFoundMessage = "Peripheral device with UID {0} not found";
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
                    string.Format(PeripheralDeviceNotFoundMessage, peripheralDeviceId));

                return serviceResult;
            }

            serviceResult.Data = peripheralDevice;

            return serviceResult;
        }
    }
}