using System.Threading.Tasks;
using GatewayManager.Data;
using GatewayManager.DataModels;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using WebSiteManager.Services;

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
    }
}