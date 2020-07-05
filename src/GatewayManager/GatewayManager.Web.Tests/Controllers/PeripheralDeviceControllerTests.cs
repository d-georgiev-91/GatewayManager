using System.Threading.Tasks;
using GatewayManager.Services;
using GatewayManager.Web.Controllers;
using GatewayManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

namespace GatewayManager.Web.Tests.Controllers
{
    [TestFixture]
    public class PeripheralDeviceControllerTests : ControllerTestsBase
    {
        private IPeripheralDeviceService _peripheralDeviceService;
        private PeripheralDeviceController _peripheralDeviceController;

        [SetUp]
        public void SetUp()
        {
            _peripheralDeviceService = Substitute.For<IPeripheralDeviceService>();
            var mapper = CreateAndConfigMapper();
            _peripheralDeviceController = new PeripheralDeviceController(_peripheralDeviceService, mapper);
        }

        [Test]
        public async Task WhenCreateIsCalledOkShouldBeReturnedAndAddAsyncShouldBeCalled()
        {
            var actionResult = await _peripheralDeviceController.Create(new PeripheralDeviceCreateModel()) as OkResult;

            await _peripheralDeviceService.Received(1).AddAsync(Arg.Any<DataModels.PeripheralDevice>());
            Assert.That(actionResult, Is.Not.Null);
        }
    }
}