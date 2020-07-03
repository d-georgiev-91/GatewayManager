using System;
using System.Linq;
using FluentValidation;

namespace GatewayManager.Web.Validators
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> IPv4String<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, ip, context) =>
            {
                context.MessageFormatter.AppendArgument("IP", ip);

                if (string.IsNullOrWhiteSpace(ip))
                {
                    return false;
                }

                var bytes = ip.Split('.', StringSplitOptions.RemoveEmptyEntries);
                if (bytes.Length != 4)
                {
                    return false;
                }

                return bytes.All(r => byte.TryParse(r, out _));
            }).WithMessage("{PropertyName} is invalid ip address: {IP}");
        }
    }
}