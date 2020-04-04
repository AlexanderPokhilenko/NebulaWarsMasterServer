using AmoebaGameMatcherServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class MatchCreationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<MatchCreationInitiatorSingletonService>();
            serviceCollection.AddTransient<BattleRoyaleMatchCreatorService>();
            serviceCollection.AddTransient<IPlayerTimeoutManager, PlayerTimeoutManagerService>();
            serviceCollection.AddTransient<BattleRoyaleMatchPackerService>();
            serviceCollection.AddTransient<MatchDbWriterService>();
            serviceCollection.AddTransient<IGameServerNegotiatorService, GameServerNegotiatorService>();
        }
    }
}