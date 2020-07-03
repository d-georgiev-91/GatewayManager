using System.Collections.Generic;
using System.Linq;
using WebSiteManager.Services;

namespace GatewayManager.Services
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Errors = new Dictionary<ErrorType, ServiceResultError>();
        }

        public Dictionary<ErrorType, ServiceResultError> Errors { get; }

        public ServiceResult AddError(ErrorType type, string message)
        {
            var error = new ServiceResultError(type, message);
            AddError(error);

            return this;
        }

        public ServiceResult AddError(ServiceResultError error)
        {
            Errors.Add(error.Error, error);

            return this;
        }

        public bool HasErrors => Errors.Any();
    }

    public class ServiceResult<TData> : ServiceResult
    {
        public TData Data { get; set; }
    }
}