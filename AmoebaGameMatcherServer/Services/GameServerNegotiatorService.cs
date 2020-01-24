using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services
{
    public class GameServerNegotiatorService
    {
        private readonly HttpClient httpClient = new HttpClient();
        
        public async Task<bool> SendRoomDataToGameServer(GameRoomData data, string serverIp=null)
        {
            if (string.IsNullOrEmpty(serverIp)) serverIp = Globals.defaultGameServerIp;

            byte[] roomData = ZeroFormatterSerializer.Serialize(data);
            HttpContent content = new ByteArrayContent(roomData); 
            
            var response = await httpClient.PostAsync(serverIp, content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                //TODO убрать после тестирования
                throw new Exception($"Навернулось при отправке запроса игровому серверу. Код = {response.StatusCode}");
                return false;
            }
        }
    }
}