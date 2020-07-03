namespace GatewayManager.Services
{
    public class ServiceResultError
    {
        public ServiceResultError(ErrorType error, string message)
        {
            Error = error;
            Message = message;
        }

        public ErrorType Error { get; set; }

        public string Message { get; set; }
    }
}
