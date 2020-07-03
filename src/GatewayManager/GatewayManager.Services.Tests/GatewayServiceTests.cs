using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using MockQueryable.NSubstitute;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;

namespace GatewayManager.Services.Tests
{
    [TestFixture]
    public class GatewayServiceTests
    {
        private IDataService<Gateway> _gatewayDataService;
        private IGatewayService _gatewayService;

        [SetUp]
        public void SetUp()
        {
            _gatewayDataService = Substitute.For<IDataService<Gateway>>();
            _gatewayService = new GatewayService(_gatewayDataService);
        }

        [Test]
        public async Task WhenAddAsyncIsCalledForDuplicateSerialNumberThenServiceResultWithErrorShouldBeReturnedAndNothingShouldBeAdded()
        {
            var gateway = new Gateway
            {
                SerialNumber = "Yd61wl8Voai3IFH",
                Name = "Uber",
                IPv4Address = "127.0.0.0"
            };

            _gatewayDataService.GetByIdAsync(Arg.Any<object>()).Returns(gateway);

            var result = await _gatewayService.AddAsync(gateway);

            _gatewayDataService.DidNotReceive().AddAsync(Arg.Any<Gateway>());
            _gatewayDataService.DidNotReceive().SaveChangesAsync();

            Assert.That(result.HasErrors, Is.True);
            Assert.That(result.Errors, Does.ContainKey(ErrorType.InvalidInput));
        }

        [Test]
        public async Task WhenAddAsyncIsCalledForNonDuplicateSerialNumberThenServiceResultWithoutErrorsAndGatewayShouldBeAdded()
        {
            var gateway = new Gateway
            {
                SerialNumber = "Yd61wl8Voai3IFH",
                Name = "Uber",
                IPv4Address = "127.0.0.0"
            };

            _gatewayDataService.GetByIdAsync(Arg.Any<object>()).ReturnsNull();

            var result = await _gatewayService.AddAsync(gateway);

            _gatewayDataService.Received(1).AddAsync(Arg.Any<Gateway>());
            _gatewayDataService.Received(1).SaveChangesAsync();

            Assert.That(result.HasErrors, Is.False);
        }

        [Test]
        public async Task WhenFindAsyncIsCalledAndThereIsNoMatchingGatewayThenServiceResultWithErrorShouldBeReturnedAndDataShouldBeEmpty()
        {
            const string serialNumber = "serial number";
            var gatewaysMock = new List<Gateway>().AsQueryable().BuildMock();
            _gatewayDataService.Filter(Arg.Any<Expression<Func<Gateway, bool>>>(),
                Arg.Any<Expression<Func<Gateway, ICollection<PeripheralDevice>>>>())
                .Returns(gatewaysMock);

            var result = await _gatewayService.FindAsync(serialNumber);

            Assert.That(result.HasErrors, Is.True);
            Assert.That(result.Errors, Contains.Key(ErrorType.NotFound));
            Assert.That(result.Data, Is.Null);
        }

        [Test]
        public async Task WhenFindAsyncIsCalledAndThereIsMatchingGatewayThenServiceResultWithNoErrorShouldBeReturnedAndDataShouldBeNotEmpty()
        {
            const string serialNumber = "serial number";
            var gateway = new Gateway
            {
                SerialNumber = serialNumber,
                Name = "Uber",
                IPv4Address = "127.0.0.0",
                PeripheralDevices = new List<PeripheralDevice>
                {
                    new PeripheralDevice
                    {
                        Uid = 1,
                        DateCreated = DateTime.Now,
                        IsOnline = true,
                        Vendor = "Sony"
                    }
                }
            };

            var gatewaysMock = new List<Gateway> { gateway }.AsQueryable().BuildMock();
            _gatewayDataService.Filter(Arg.Any<Expression<Func<Gateway, bool>>>(),
                    Arg.Any<Expression<Func<Gateway, ICollection<PeripheralDevice>>>>())
                .Returns(gatewaysMock);

            var result = await _gatewayService.FindAsync(serialNumber);

            Assert.That(result.HasErrors, Is.False);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Is.EqualTo(gateway));
        }
    }
}