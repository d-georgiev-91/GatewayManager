using System.Threading.Tasks;

namespace GatewayManager.Services
{
    public interface IGatewayDeviceManager
    {
        Task<ServiceResult> AssignPeripheralDevice(string serialNumber, long peripheralDeviceId);
    }
}
