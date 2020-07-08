using GatewayManager.DataModels;

namespace GatewayManager.Data
{
    public class GatewayDataService : DataService<Gateway>
    {
        public GatewayDataService(GatewayManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}