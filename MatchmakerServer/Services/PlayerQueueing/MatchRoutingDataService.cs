namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Нужен для того, чтобы узнать ip/port игрового сервера.
    /// </summary>
    public class MatchRoutingDataService
    {
        private readonly GameServersRoutingDataService serversRoutingDataService;

        public MatchRoutingDataService(GameServersRoutingDataService serversRoutingDataService)
        {
            this.serversRoutingDataService = serversRoutingDataService;
        }

        public MatchRoutingData GetMatchRoutingData()
        {
            return serversRoutingDataService.GetGameServerAddress();
        }
    }
}