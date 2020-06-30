using System;

namespace WebSiteManager.DataModels
{
    public class PeripheralDevice
    {
        public long Uid { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsOnline { get; set; }

        public string GatewaySerialNumber { get; set; }

        public Gateway Gateway { get; set; }
    }
}
