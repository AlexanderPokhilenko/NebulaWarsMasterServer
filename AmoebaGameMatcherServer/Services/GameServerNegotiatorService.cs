using System;
using System.Net;
using System.Net.Http;
using System.Threading.Channels;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services
{
    public class GameServerNegotiatorService
    {
        private readonly HttpClient httpClient = new HttpClient();
        
        public async Task SendRoomDataToGameServerAsync(GameRoomData data)
        {
            if (string.IsNullOrEmpty(data.GameServerIp)) 
                throw new Exception("При отправке данных на игровой сервер ip не указан");
            
            string serverIp = data.GameServerIp+":"+Globals.defaultGameServerHttpPort.ToString();
            byte[] roomData = ZeroFormatterSerializer.Serialize(data);
            Console.WriteLine($"Отправка данных на игровой сервер по ip = {serverIp}");
            HttpContent content = new ByteArrayContent(roomData);    
            var response = await httpClient.PostAsync(serverIp, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Получен ответ от игрового сервера. Статус = \"успешно\" ");
            }
            else
            {
                //TODO убрать после тестирования
                throw new Exception($"Навернулось при отправке запроса игровому серверу. Код = {response.StatusCode}");
            }
        }
    }
}