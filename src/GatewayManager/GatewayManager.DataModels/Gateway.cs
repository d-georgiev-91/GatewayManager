using System.Collections.Generic;

namespace GatewayManager.DataModels
{
    public class Gateway
    {
        public string SerialNumber { get; set; }

        public string Name { get; set; }

        public string IPv4Address { get; set; }

        public ICollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
