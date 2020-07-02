using AmoebaGameMatcherServer.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class WarshipUpgradeFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<WarshipImprovementFacadeService, WarshipImprovementFacadeService>();
        }
    }
}