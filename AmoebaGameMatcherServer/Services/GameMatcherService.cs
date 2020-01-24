using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Experimental;

//TODO говнокод
//TODO слишком большой класс

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
            bool thereIsAFullSetOfPlayers = dataService.UnsortedPlayers.Count >= Globals.NumbersOfPlayersInRoom;
            if ( thereIsAFullSetOfPlayers) CreateRoom(Globals.NumbersOfPlayersInRoom);
        }

        /// <summary>
        /// Создаёт комнату если есть хотя бы один игрок.
        /// </summary>
        public void CreateRoom(int maxNumberOfPlayers)
        {
            //Достать игроков maxNumberOfPlayers игрокв
            var playersInfo = GetPlayersFromQueue(maxNumberOfPlayers);
            if (playersInfo.Count == 0)
            {
                throw new Exception("Не удалось достать игроков из коллекции. " +
                                    "При принудительном создании комнаты");
            }
                
            //Дополнить коллекцию ботами, если не хватает
            if (playersInfo.Count != maxNumberOfPlayers)
            {
                int botsCount = maxNumberOfPlayers - playersInfo.Count; 
                AddBotsToList(ref playersInfo, botsCount);
            }
            
            //Создать комнату с игроками
            int newRoomNumber = GameRoomIdGenerator.CreateGameRoomNumber();
            GameRoomData gameRoomData = new GameRoomData
            {
                Players = playersInfo.ToArray(),
                GameRoomNumber = newRoomNumber,
                GameServerIp = "localhost:48956/"
            };
            
            //Поставить игрокам статус "В бою"
            bool success = TryAddPlayersToTheListOfPlayersInBattle(playersInfo, newRoomNumber);

            if(!success)
                throw new Exception("Не удалось поставить всем игрокам статус \"В бою\"ю");
            
            //Положить кооманту в коллекцию
            if (dataService.GameRoomsData.TryAdd(gameRoomData.GameRoomNumber, gameRoomData))
            {
                //всё отработало нормально
            }
            else
            {
                //не удалось положить комнату в коллекцию
                throw new Exception("Не удалось положить комнату в коллекцию");
            }
        }

        void AddBotsToList(ref List<PlayerInfoForGameRoom> players, int botsCount)
        {
            Random random = new Random();
            for (int i = 0; i < botsCount; i++)
            {
                var dich = new PlayerInfoForGameRoom
                {
                    IsBot = true,
                    PlayerLogin = "Рандомное имя для бота "+ random.Next(1,Int32.MaxValue).ToString() 
                };
                players.Add(dich);
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
        
        bool TryAddPlayersToTheListOfPlayersInBattle(List<PlayerInfoForGameRoom> players, int gameRoomNumber)
        {
            bool success = true;
            foreach (var player in players)
            {
                if (dataService.PlayersInGameRooms.TryAdd(player.PlayerLogin, gameRoomNumber))
                {
                    
                }
                else
                {
                    success = false;
                }
            }
            return success;
        }

        public bool PlayerInQueue(string playerId)
        {
            Console.WriteLine($"Обработка запроса от игрока. кол-во в очереди {dataService.UnsortedPlayers.Count}. ");
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