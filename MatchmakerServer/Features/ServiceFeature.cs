using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public abstract class ServiceFeature
    {
        public abstract void Add(IServiceCollection serviceCollection);
    }
}