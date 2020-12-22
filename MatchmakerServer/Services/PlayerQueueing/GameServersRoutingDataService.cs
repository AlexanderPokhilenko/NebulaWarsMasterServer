using System.Net.Http;
using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class GameServersRoutingDataService
    {
        public MatchRoutingData GetGameServerAddress()
        {
            using (var httpClient = new HttpClient())
            {
                const string url = Globals.GetServerAddressUrl + "?secret=" + Globals.GameServerSecret;
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var address = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrWhiteSpace(address))
                    {
                        return new MatchRoutingData(address, Globals.DefaultGameServerUdpPort);
                    }
                }
            }

            return new MatchRoutingData(Globals.DefaultGameServerIp, Globals.DefaultGameServerUdpPort);
        }
    }
}