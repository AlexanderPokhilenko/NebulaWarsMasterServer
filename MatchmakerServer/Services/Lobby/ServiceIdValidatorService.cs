namespace AmoebaGameMatcherServer.Services
{
    public interface IServiceIdValidator
    {
        bool Validate(string serviceId);
    }

    public class ServiceIdValidatorServiceStub:IServiceIdValidator
    {
        public bool Validate(string serviceId)
        {
            return !string.IsNullOrEmpty(serviceId);
        }
    }
    
    public class ServiceIdValidatorService:IServiceIdValidator
    {
        public bool Validate(string serviceId)
        {
            return serviceId?.Length > 10;
        }
    }
}