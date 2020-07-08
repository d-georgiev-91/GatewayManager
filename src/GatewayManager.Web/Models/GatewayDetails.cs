using System.Collections.Generic;

namespace GatewayManager.Web.Models
{
    public class GatewayDetails : Gateway
    {
        public IReadOnlyCollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}