using AmoebaGameMatcherServer.Services.Lootbox;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class LootboxFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LootboxFacadeService>();
            serviceCollection.AddTransient<SmallLootboxDataFactory>();
            serviceCollection.AddTransient<LootboxDbWriterService>();
            serviceCollection.AddTransient<LootboxResourcesFactory>();
            serviceCollection.AddTransient<LootboxResourceTypeFactory>();
        }
    }
}