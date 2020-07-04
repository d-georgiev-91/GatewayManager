using System;

namespace GatewayManager.Web.Controllers
{
    public class PeripheralDeviceCreateModel
    {
        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsOnline { get; set; }
    }
}