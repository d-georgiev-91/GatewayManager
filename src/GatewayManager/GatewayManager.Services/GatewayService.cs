using System.Linq;
using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using GatewayManager.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace GatewayManager.Services
{
    public class GatewayService : IGatewayService
    {
        private const int AllowedPeripheralDevicesPerGateway = 10;
        private const string GatewayWithSerialNotFoundMessage = "No such gateway with serial number: {0}";
        private const string GatewayWithSuchSerialNumberAlreadyExistsMessage = "Gateway with such serial number already exists";
        private const string ExceededAllowedDevicesPerGatewayMessage = "Only {0} peripheral devices are allowed per gateway";

        private readonly IDataService<Gateway> _gatewayDataService;

        public GatewayService(IDataService<Gateway> gatewayDataService) => _gatewayDataService = gatewayDataService;

        public async Task<ServiceResult> AddAsync(Gateway gateway)
        {
            var result = new ServiceResult();
            var existingGateway = await _gatewayDataService.GetByIdAsync(gateway.SerialNumber);

            if (existingGateway != null)
            {
                return result.AddError(ErrorType.InvalidInput, GatewayWithSuchSerialNumberAlreadyExistsMessage);
            }

            await _gatewayDataService.AddAsync(gateway);
            await _gatewayDataService.SaveChangesAsync();

            return result;
        }

        public async Task<ServiceResult<Gateway>> FindAsync(string serialNumber)
        {
            var result = new ServiceResult<Gateway>();

            var gateway = await _gatewayDataService.Filter(g => g.SerialNumber == serialNumber,
                    g => g.PeripheralDevices)
                .SingleOrDefaultAsync();

            if (gateway != null)
            {
                result.Data = gateway;
            }
            else
            {
                result.AddError(ErrorType.NotFound, string.Format(GatewayWithSerialNotFoundMessage, serialNumber));
            }

            return result;
        }

        public async Task<ServiceResult<Paginated<Gateway>>> GetAllAsync(Page page)
        {
            var serviceResult = new ServiceResult<Paginated<Gateway>>();

            var gateways = _gatewayDataService.GetAll();

            var gatewaysCount = gateways.Count();

            gateways = gateways
                .Take(page.Size)
                .Skip(page.Index * page.Size);

            serviceResult.Data = new Paginated<Gateway>
            {
                Data = await gateways.ToListAsync(),
                TotalCount = gatewaysCount
            };

            return serviceResult;
        }

        public async Task<ServiceResult> AddPeripheralDeviceAsync(string gatewaySerialNumber, PeripheralDevice peripheralDevice)
        {
            var serviceResult = new ServiceResult();
            var gateway = await _gatewayDataService.GetByIdAsync(gatewaySerialNumber);

            if (gateway == null)
            {
                serviceResult.AddError(ErrorType.NotFound, string.Format(GatewayWithSerialNotFoundMessage, gatewaySerialNumber));

                return serviceResult;
            }

            if (gateway.PeripheralDevices.Count >= AllowedPeripheralDevicesPerGateway)
            {
                serviceResult.AddError(ErrorType.InvalidInput,
                    string.Format(ExceededAllowedDevicesPerGatewayMessage, AllowedPeripheralDevicesPerGateway));

                return serviceResult;
            }

            gateway.PeripheralDevices.Add(peripheralDevice);
            await _gatewayDataService.SaveChangesAsync();

            return serviceResult;
        }

        public async Task RemovePeripheralDeviceAsync(Gateway gateway, PeripheralDevice peripheralDevice)
        {
            gateway.PeripheralDevices.Remove(peripheralDevice);
            await _gatewayDataService.SaveChangesAsync();
        }
    }
}
