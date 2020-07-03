using System.Threading.Tasks;
using GatewayManager.DataModels;
using GatewayManager.Services.Models;

namespace GatewayManager.Services
{
    public interface IGatewayService
    {
        Task<ServiceResult> AddAsync(Gateway gateway);
        
        Task<ServiceResult<Gateway>> FindAsync(string serialNumber);
        
        Task<ServiceResult<Paginated<Gateway>>> GetAllAsync(Page page);
    }
}