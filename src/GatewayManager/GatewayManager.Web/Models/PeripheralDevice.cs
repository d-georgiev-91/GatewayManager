using System;

namespace GatewayManager.Web.Models
{
    public class PeripheralDevice
    {
        public long Uid { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsOnline { get; set; }
    }
}