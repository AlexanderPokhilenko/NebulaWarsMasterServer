namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Нужен для того, чтобы узнать ip/port игрового сервера.
    /// </summary>
    public class MatchRoutingDataService
    {
        private readonly GameServersManagerService serversManagerService;

        public MatchRoutingDataService(GameServersManagerService serversManagerService)
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
}