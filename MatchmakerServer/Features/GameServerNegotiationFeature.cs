using AmoebaGameMatcherServer.Services.GameServerNegotiation;
using Microsoft.Extensions.DependencyInjection;

namespace AmoebaGameMatcherServer
{
    public class GameServerNegotiationFeature:ServiceFeature
    {
        public override void Add(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IGameServerNegotiatorService, GameServerNegotiatorService>();
        }
    }
}