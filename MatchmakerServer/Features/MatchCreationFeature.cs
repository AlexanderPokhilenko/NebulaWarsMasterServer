using AmoebaGameMatcherServer.Services.MatchCreation;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class MatchCreationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BattleRoyaleMatchCreatorService>();
            serviceCollection.AddTransient<BattleRoyaleBotFactoryService>();
            serviceCollection.AddTransient<MatchDbWriterService>();
        }
    }
}