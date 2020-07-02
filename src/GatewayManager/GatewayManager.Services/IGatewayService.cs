using System.Threading.Tasks;
using GatewayManager.DataModels;

namespace GatewayManager.Services
{
    public interface IGatewayService
    {
        Task Add(Gateway gateway);
    }
}