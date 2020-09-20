using AmoebaGameMatcherServer.Services.MatchFinishing;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class MatchFinishingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<BattleRoyaleMatchFinisherService>();
            serviceCollection.AddTransient<BattleRoyaleMatchRewardCalculatorService>();
            serviceCollection.AddTransient<PlayerMatchResultDbReaderService>();
            serviceCollection.AddTransient<WarshipRatingReaderService>();
        }
    }
}