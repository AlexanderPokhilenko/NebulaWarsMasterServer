using System;
using System.Collections.Generic;
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
            AddPlayerToQueue(playerId);
            //так как коллекция могла наполнится можно попробовать создать комнату
            TryCreateRoom();
        }

        public GameRoomData GetGameRoomData(string playerId)
        {
            bool thereIsInformationAboutThisPlayer = dataService.PlayersGameRooms.Keys.Contains(playerId);
            if (thereIsInformationAboutThisPlayer)
            {
                dataService.GameRoomsData.TryGetValue(playerId, out var gameRoomData);
                return gameRoomData;
            }
            else
            {
                return null;
            }
        }
        
        
        
        
        void AddPlayerToQueue(string playerId)
        {
            PlayerRequest playerRequest = new PlayerRequest
            {
                PlayerId = playerId,
                Time = DateTime.UtcNow
            };
            dataService.UnsortedPlayers.Enqueue(playerRequest);
        }

        void TryCreateRoom()
        {
            if (dataService.UnsortedPlayers.Count >= 10)
            {
                List<PlayerRequest> playerRequests = new List<PlayerRequest>();
                for (int i = 0; i < 10; i++)
                {
                    if (dataService.UnsortedPlayers.TryDequeue(out var _playerRequest))
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
    }
}