using AmoebaGameMatcherServer.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class LootboxFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LootboxFacadeService>();
            serviceCollection.AddTransient<SmallLootboxDataFactory>();
            serviceCollection.AddTransient<SmallLootboxOpenAllowingService>();
            // serviceCollection.AddTransient<LootboxDbWriterService>();
        }
    }
}