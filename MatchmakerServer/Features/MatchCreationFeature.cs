using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.MatchCreation;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class MatchCreationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BattleRoyaleMatchCreatorService>();
            serviceCollection.AddTransient<BattleRoyaleMatchPackerService>();
            serviceCollection.AddTransient<MatchDbWriterService>();
        }
    }
}