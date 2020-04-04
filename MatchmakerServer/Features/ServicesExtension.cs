using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public static class ServicesExtension
    {
        public static void AddFeature(this IServiceCollection serviceCollection, ServiceFeature serviceFeature)
        {
            serviceFeature.Add(serviceCollection);
        }
    }
}