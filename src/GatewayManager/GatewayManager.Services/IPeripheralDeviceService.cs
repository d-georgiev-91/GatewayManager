using System.Threading.Tasks;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public interface IPeripheralDeviceService
    {
        Task AddAsync(PeripheralDevice peripheralDevice);
    }
}
