using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class GameServersRoutingDataService
    {
        public MatchRoutingData GetGameServerAddress()
        {
            MatchRoutingData result = 
                new MatchRoutingData(Globals.DefaultGameServerIp, Globals.DefaultGameServerUdpPort);
                
            return result;
        }
    }
}