using System;
using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using NSubstitute;
using NUnit.Framework;

namespace GatewayManager.Services.Tests
{
    [TestFixture]
    public class PeripheralDeviceServiceTests
    {
        private IDataService<PeripheralDevice> _peripheralDeviceDataService;
        private IPeripheralDeviceService _peripheralDeviceService;

        [SetUp]
        public void SetUp()
        {
            _peripheralDeviceDataService = Substitute.For<IDataService<PeripheralDevice>>();
            _peripheralDeviceService = new PeripheralDeviceService(_peripheralDeviceDataService);
        }

        [Test]
        public async Task WhenAddAsyncIsCalledThenAddAsyncAndSaveChangesAsyncOfPeripheralDeviceServiceShouldBeCalled()
        {
            var peripheralDevice = new PeripheralDevice
            {
                Uid = 1,
                Vendor = "Sony",
                DateCreated = DateTime.Now,
                IsOnline = false
            };

            await _peripheralDeviceService.AddAsync(peripheralDevice);

            await _peripheralDeviceDataService.Received(1).AddAsync(peripheralDevice);
            await _peripheralDeviceDataService.Received(1).SaveChangesAsync();
        }
    }
}