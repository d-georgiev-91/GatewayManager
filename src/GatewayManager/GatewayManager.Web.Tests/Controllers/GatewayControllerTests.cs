﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GatewayManager.Services;
using GatewayManager.Web.Controllers;
using GatewayManager.Web.Models;
using GatewayManager.Web.Tests.AutoMapper;
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

        private IMapper CreateAndConfigMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestsProfile>();
            });

            return config.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            _gatewayService = Substitute.For<IGatewayService>();
            var mapper = CreateAndConfigMapper();
            _gatewayController = new GatewayController(_gatewayService, mapper);
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

            _gatewayService.AddAsync(Arg.Any<DataModels.Gateway>())
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

            _gatewayService.AddAsync(Arg.Any<DataModels.Gateway>())
                .Returns(new ServiceResult().AddError(error));

            var actionResult = await _gatewayController.Create(gateway) as BadRequestObjectResult;

            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult.Value, Is.EqualTo(error.Message));
        }

        [Test]
        public async Task WhenGatewaySearchedBySerialNumberDoesNotExistThenNotFoundObjectResultShouldBeReturned()
        {
            const string serialNumber = "serial number";
            var notFoundError = new ServiceResultError(ErrorType.NotFound, "Error");
            var serviceResult = new ServiceResult<DataModels.Gateway>();
            serviceResult.AddError(notFoundError);
            _gatewayService.FindAsync(Arg.Any<string>()).Returns(serviceResult);
            var result = await _gatewayController.GetDetails(serialNumber) as NotFoundObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(notFoundError));
        }

        [Test]
        public async Task WhenGatewaySearchedBySerialNumberDoesExistThenOkObjectResultShouldWithDataBeReturned()
        {
            const string serialNumber = "serial number";
            var gateway = new DataModels.Gateway
            {
                SerialNumber = "Yd61wl8Voai3IFH",
                Name = "Uber",
                IPv4Address = "127.0.0.0",
                PeripheralDevices = new List<DataModels.PeripheralDevice>
                {
                    new DataModels.PeripheralDevice
                    {
                        Uid = 1,
                        DateCreated = DateTime.Now,
                        IsOnline = true,
                        Vendor = "Sony"
                    }
                }
            };

            var serviceResult = new ServiceResult<DataModels.Gateway> { Data = gateway };

            _gatewayService.FindAsync(Arg.Any<string>()).Returns(serviceResult);
            var result = await _gatewayController.GetDetails(serialNumber) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.Not.Null);
        }
    }
}