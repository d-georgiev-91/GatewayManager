using System.Threading.Tasks;
using AutoMapper;
using GatewayManager.DataModels;
using GatewayManager.Services;
using GatewayManager.Web.Controllers;
using GatewayManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using WebSiteManager.Services;

namespace GatewayManager.Web.Tests.Controllers
{
    [TestFixture]
    public class GatewayControllerTests
    {
        private IGatewayService _gatewayService;
        private GatewayController _gatewayController;
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            _gatewayService = Substitute.For<IGatewayService>();
            _mapper = Substitute.For<IMapper>();
            _gatewayController = new GatewayController(_gatewayService, _mapper);
        }

        [Test]
        public async Task WhenGatewayWithNonDuplicateSerialNumberIsAddedCreateResponseShouldBeReturned()
        {
            var gateway = new GatewayCreateModel
            {
                SerialNumber = "Yd61wl8Voai3IFH",
                Name = "Uber",
                IPv4Address = "127.0.0.0"
            };

            _gatewayService.AddAsync(Arg.Any<Gateway>())
                .Returns(new ServiceResult());

            var actionResult = await _gatewayController.Create(gateway) as CreatedAtActionResult;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.ActionName, Is.EqualTo(nameof(GatewayController.GetDetails)));
            Assert.That(actionResult.RouteValues, Contains.Key("serialNumber"));
            Assert.That(actionResult.RouteValues["serialNumber"], Is.EqualTo(gateway.SerialNumber));
            Assert.That(actionResult.Value, Is.EqualTo(gateway));
        }

        [Test]
        public async Task WhenGatewayWithDuplicateSerialNumberIsAddedThenBadRequestShouldBeReturned()
        {
            var gateway = new GatewayCreateModel
            {
                SerialNumber = "Yd61wl8Voai3IFH",
                Name = "Uber",
                IPv4Address = "127.0.0.0"
            };

            var error = new ServiceResultError(ErrorType.InvalidInput, "Duplicate gateway");

            _gatewayService.AddAsync(Arg.Any<Gateway>())
                .Returns(new ServiceResult().AddError(error));

            var actionResult = await _gatewayController.Create(gateway) as BadRequestObjectResult;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Value, Is.EqualTo(error.Message));
        }
    }
}