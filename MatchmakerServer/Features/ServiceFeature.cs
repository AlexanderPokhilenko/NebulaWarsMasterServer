using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public abstract class ServiceFeature
    {
        public abstract void Add(IServiceCollection serviceCollection);
    }
}