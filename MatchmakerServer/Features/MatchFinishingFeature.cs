using AmoebaGameMatcherServer.Services.MatchFinishing;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer.Features
{
    public class MatchFinishingFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IBattleRoyaleMatchFinisherService, BattleRoyaleMatchFinisherService>();
            serviceCollection.AddTransient<IBattleRoyaleMatchRewardCalculatorService, BattleRoyaleMatchRewardCalculatorService>();
            serviceCollection.AddTransient<IPlayerMatchResultDbReaderService, PlayerMatchResultDbReaderService>();
            serviceCollection.AddTransient<IWarshipRatingReaderService, WarshipRatingReaderService>();
        }
    }
}