using AmoebaGameMatcherServer.Services.Experimental;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class WarshipUpgradeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<WarshipImprovementFacadeService, WarshipImprovementFacadeService>();
        }
    }
}