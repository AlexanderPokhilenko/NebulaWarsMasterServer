using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Experimental;

//TODO ооп пошло по жопе

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
            TryCreateRoom();
        }


        void AddPlayerToQueue(string playerId)
        {
            if (!PlayerInQueue(playerId))
            {
                Console.WriteLine("AddPlayerToQueue");
                PlayerRequest playerRequest = new PlayerRequest
                {
                    PlayerId = playerId,
                    Time = DateTime.UtcNow
                };
                dataService.UnsortedPlayers.Enqueue(playerRequest);    
            }
        }

        void TryCreateRoom()
        {
            Console.WriteLine("TryCreateRoom");
            if (dataService.UnsortedPlayers.Count >= Globals.NumbersOfPlayersInRoom)
            {
                var playersInfo = GetPlayersFromQueue(Globals.NumbersOfPlayersInRoom);
                int newRoomNumber = GameRoomIdGenerator.CreateGameRoomNumber();

                AddPlayersToTheListOfPlayersInBattle(playersInfo, newRoomNumber);
                
                GameRoomData gameRoomData = new GameRoomData
                {
                    Players = playersInfo.ToArray(),
                    GameRoomNumber = newRoomNumber,
                    GameServerIp = "localhost:48956/"
                };

                dataService.GameRoomsData.TryAdd(gameRoomData.GameRoomNumber, gameRoomData);
            }
        }

        List<PlayerInfoForGameRoom> GetPlayersFromQueue(int numberOfPlayers)
        {
            List<PlayerInfoForGameRoom> playersInfo = new List<PlayerInfoForGameRoom>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (dataService.UnsortedPlayers.TryDequeue(out var playerRequest))
                {
                    var dich = new PlayerInfoForGameRoom
                    {
                        IsBot = false,
                        PlayerLogin = playerRequest.PlayerId
                    };
                    playersInfo.Add(dich);
                }
            }

            return playersInfo;
        }
        
        void AddPlayersToTheListOfPlayersInBattle(List<PlayerInfoForGameRoom> players, int gameRoomNumber)
        {
            foreach (var player in players)
            {
                dataService.PlayersInGameRooms.TryAdd(player.PlayerLogin, gameRoomNumber);
            }
        }

        public bool PlayerInQueue(string playerId)
        {
            Console.WriteLine($"Обработка запроса от игрока. кол-во в очереди {dataService.UnsortedPlayers.Count}. " +
                              $"id data = {dataService.Id}");
            var player = dataService.UnsortedPlayers.SingleOrDefault(request => request.PlayerId == playerId);
            return  player != null;
        }

        public bool PlayerInBattle(string playerId)
        {
            return dataService.PlayersInGameRooms.ContainsKey(playerId);
        }

        public GameRoomData GetRoomData(string playerId)
        {
            if (PlayerInBattle(playerId))
            {
                dataService.PlayersInGameRooms.TryGetValue(playerId, out int roomNumber);
                dataService.GameRoomsData.TryGetValue(roomNumber, out var roomData);
                return roomData;
            }
            else
            {
                throw new Exception($"Игрок с id={playerId} не находится в словаре игроков в бою");
            }
        }

        public void DeleteRoom(int roomNumber)
        {
            var deletingRoom = dataService.GameRoomsData[roomNumber];
            
            //Удалить всех игроков
            foreach (var player in deletingRoom.Players)
            {
                dataService.PlayersInGameRooms.TryRemove(player.PlayerLogin, out var dich);
            }
            //Удалить комнату
            dataService.GameRoomsData.TryRemove(roomNumber, out var sich);
        }
    }
}