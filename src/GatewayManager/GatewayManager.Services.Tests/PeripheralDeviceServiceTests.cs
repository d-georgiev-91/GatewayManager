using System;
using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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

        [Test]
        public async Task WhenGetByIdIsCalledAndThereIsExistingPeripheralDeviceByGivenIdThenServiceResultWithNoErrorsShouldBeReturnedAndDataShouldBeSPeripheralDevice()
        {
            const long peripheralDeviceId = 5;
            var peripheralDevice = new PeripheralDevice();
            _peripheralDeviceDataService.GetByIdAsync(Arg.Any<object>()).Returns(peripheralDevice);

            var serviceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            Assert.That(serviceResult.HasErrors, Is.False);
            Assert.That(serviceResult.Data, Is.SameAs(peripheralDevice));
        }

        [Test]
        public async Task WhenGetByIdIsCalledAndThereIsNoPeripheralDeviceByGivenIdThenServiceResultWithNotFoundErrorShouldBeReturned()
        {
            const long peripheralDeviceId = 5;
            _peripheralDeviceDataService.GetByIdAsync(Arg.Any<object>()).ReturnsNull();

            var serviceResult = await _peripheralDeviceService.GetByIdAsync(peripheralDeviceId);

            Assert.That(serviceResult.HasErrors, Is.True);
            Assert.That(serviceResult.Errors, Contains.Key(ErrorType.NotFound));
        }
    }
}