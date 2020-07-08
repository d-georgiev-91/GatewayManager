using GatewayManager.DataModels;

namespace GatewayManager.Data
{
    public class PeripheralDeviceDataService : DataService<PeripheralDevice>
    {
        public PeripheralDeviceDataService(GatewayManagerDbContext dbContext) : base(dbContext)
        {
        }
    }
}