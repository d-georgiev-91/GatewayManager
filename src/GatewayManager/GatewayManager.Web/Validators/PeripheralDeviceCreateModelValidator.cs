using System;
using FluentValidation;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.Validators
{
    public class PeripheralDeviceCreateModelValidator : AbstractValidator<PeripheralDeviceCreateModel>
    {
        public PeripheralDeviceCreateModelValidator()
        {
            RuleFor(peripheralDevice => peripheralDevice.Vendor).NotEmpty();
            RuleFor(peripheralDevice => peripheralDevice.DateCreated).LessThanOrEqualTo(DateTime.Now);
        }
    }
}
