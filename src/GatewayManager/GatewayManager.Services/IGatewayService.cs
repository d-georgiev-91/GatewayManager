using System.Threading.Tasks;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public interface IGatewayService
    {
        Task<ServiceResult> AddAsync(Gateway gateway);
    }
}