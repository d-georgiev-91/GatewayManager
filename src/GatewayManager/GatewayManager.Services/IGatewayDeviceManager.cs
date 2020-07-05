using System.Threading.Tasks;

namespace GatewayManager.Services
{
    public interface IGatewayDeviceManager
    {
        Task<ServiceResult> AssignPeripheralDeviceAsync(string serialNumber, long peripheralDeviceId);
        
        Task<ServiceResult> RemovePeripheralDeviceAsync(string serialNumber, long peripheralDeviceId);
    }
}
