using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
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