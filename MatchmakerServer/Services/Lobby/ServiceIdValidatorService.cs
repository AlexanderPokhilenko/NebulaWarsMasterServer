using System;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Проверяет, что такой serviceId существует.
    /// </summary>
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
    
    // public class ServiceIdValidatorService:IServiceIdValidator
    // {
    //     public bool Validate([NotNull] string serviceId)
    //     {
    //         bool success = serviceId.Length >= 10;
    //         if (!success)
    //         {
    //             Console.WriteLine("Длина serviceId = "+serviceId.Length);
    //         }
    //         return success;
    //     }
    // }
}