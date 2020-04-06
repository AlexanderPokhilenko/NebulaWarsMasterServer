using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class GameServersRoutingDataService
    {
        public (string ip, int port) GetGameServerAddress()
        {
            (string ip, int port) result;
            result.ip = Globals.DefaultGameServerIp;
            result.port = Globals.DefaultGameServerUdpPort;
            return result;
        }
    }
}