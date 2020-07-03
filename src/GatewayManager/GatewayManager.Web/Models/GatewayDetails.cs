using System.Collections.Generic;

namespace GatewayManager.Web.Models
{
    public class GatewayDetails
    {
        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IPv4Address { get; set; }

        public IReadOnlyCollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}