using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Utils;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за отправку http сообщения с данными про матч на гейм сервер для запуска боя.
    /// </summary>
    public class GameServerNegotiatorService:IGameServerNegotiatorService
    {
        private readonly HttpClient httpClient = new HttpClient();
        
        public async Task SendRoomDataToGameServerAsync(BattleRoyaleMatchData data)
        {
            if (string.IsNullOrEmpty(data.GameServerIp))
            {
                throw new Exception("При отправке данных на игровой сервер ip не указан");
            }
            
            string serverIp = $"http://{data.GameServerIp}:{Globals.DefaultGameServerHttpPort}";
            byte[] roomData = ZeroFormatterSerializer.Serialize(data);
            Console.WriteLine($"Отправка данных на игровой сервер по ip = {serverIp} количество байт = {roomData.Length}");
            HttpContent content = new ByteArrayContent(roomData);
            var response = await httpClient.PostAsync(serverIp, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Получен ответ от игрового сервера. Статус = \"успешно\" ");
                byte[] responseData = await response.Content.ReadAsByteArrayAsync();
                var r = ZeroFormatterSerializer.Deserialize<GameRoomValidationResult>(responseData);
                Console.WriteLine($"ResultEnum = {r.ResultEnum}");
            }
            else
            {
                //TODO убрать после тестирования
                throw new Exception($"Брошено исключение при отправке запроса игровому серверу. " +
                                    $"Код = {response.StatusCode}");
            }
        }
    }
}