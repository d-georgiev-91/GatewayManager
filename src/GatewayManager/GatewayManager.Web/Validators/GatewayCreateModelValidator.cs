using FluentValidation;
using GatewayManager.Web.Models;

namespace GatewayManager.Web.Validators
{
    public class GatewayCreateModelValidator : AbstractValidator<GatewayCreateModel>
    {
        public GatewayCreateModelValidator()
        {
            RuleFor(gateway => gateway.SerialNumber).NotEmpty();
            RuleFor(gateway => gateway.Name).NotEmpty();
            RuleFor(gateway => gateway.IPv4Address).NotEmpty().IPv4String();
        }
    }
}
