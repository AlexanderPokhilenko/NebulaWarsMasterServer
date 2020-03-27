using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    public class MatchRoutingData
    {
        public string GameServerIp;
        public int GameServerPort;
    }
    
    //есть
    public class MatchmakerDichService
    {
        private readonly GameServersManagerService serversManagerService;

        public MatchmakerDichService(GameServersManagerService serversManagerService)
        {
            this.serversManagerService = serversManagerService;
        }

        public MatchRoutingData GetMatchRoutingData()
        {
            var result = new MatchRoutingData();
            var (ip, port) = serversManagerService.GetGameServerAddress();
            result.GameServerIp = ip;
            result.GameServerPort = port;
            return result;
        }
    }

    //есть
    public class GameServersManagerService
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