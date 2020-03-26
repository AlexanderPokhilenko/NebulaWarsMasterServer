using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    public class GameServersManagerService
    {
        public GameServerData GetGameServerData()
        {
            return new GameServerData()
            {
                GameServerIp = Globals.DefaultGameServerIp,
                GameServerPort = Globals.DefaultGameServerUdpPort
            };
        }
    }
}