namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class MatchRoutingData
    {
        public string GameServerIp;
        public int GameServerPort;

        public MatchRoutingData(string gameServerIp, int gameServerPort)
        {
            GameServerIp = gameServerIp;
            GameServerPort = gameServerPort;
        }
    }
}