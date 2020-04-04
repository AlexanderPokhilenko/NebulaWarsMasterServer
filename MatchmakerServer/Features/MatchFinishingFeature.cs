using AmoebaGameMatcherServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class MatchFinishingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BattleRoyaleMatchFinisherService>();
            serviceCollection.AddTransient<BattleRoyaleMatchRewardService>();
            serviceCollection.AddTransient<PlayerMatchResultDbReaderService>();
        }
    }
}