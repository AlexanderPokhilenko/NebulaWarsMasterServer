using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherService
    {
        private readonly GameMatcherDataService dataService;
        
        public GameMatcherService(GameMatcherDataService dataService)
        {
            this.dataService = dataService;
        }

        public void RegisterPlayer(string playerId)
        {
            //Добавление игрока в очередь
            PlayerRequest playerRequest = new PlayerRequest
            {
                PlayerId = playerId,
                Time = DateTime.UtcNow
            };
            dataService.unsortedPlayers.Enqueue(playerRequest);
            //Попытка создать комнату
            if (dataService.unsortedPlayers.Count >= 10)
            {
                List<PlayerRequest> playerRequests = new List<PlayerRequest>();
                for (int i = 0; i < 10; i++)
                {
                    if (dataService.unsortedPlayers.TryDequeue(out var _playerRequest))
                    {
                        //номально достали из коллекции
                        playerRequests.Add(_playerRequest);
                    }
                }

                List<PlayerInfoForGameRoom> playersInfo = new List<PlayerInfoForGameRoom>();
                foreach (var request in playerRequests)
                {
                    var dich = new PlayerInfoForGameRoom
                    {
                        IsBot = false,
                        PlayerLogin = request.PlayerId
                    };
                    playersInfo.Add(dich);
                }

                GameRoomData gameRoomData = new GameRoomData
                {
                    Players = playersInfo,
                    GameToomNumber = 0
                };

            }
        }

        public async Task<GameRoomData> GetGameRoomData(string playerId)
        {
            return null;
        }
    }
}