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
    }
}