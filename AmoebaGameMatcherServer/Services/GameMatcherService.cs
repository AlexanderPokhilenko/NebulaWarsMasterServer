using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Utils;
using Microsoft.CodeAnalysis;
using NetworkLibrary.NetworkLibrary.Http;

//TODO слишком большой класс

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherService
    {
        private readonly GameMatcherDataService dataService;
        private readonly GameServerNegotiatorService gameServerNegotiatorService;
        
        public GameMatcherService(GameMatcherDataService dataService, GameServerNegotiatorService gameServerNegotiatorService)
        {
            this.dataService = dataService;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
        }
        public void RegisterPlayer(string playerId)
        {
            AddPlayerToQueue(playerId);
            TryCreateRoom();
        }

        public bool TryRemovePlayerFromQueue(string playerId)
        {
            Console.WriteLine("Удаление игрока с id = "+playerId + " из очереди.");
            return dataService.UnsortedPlayers.TryRemove(playerId, out DateTime value);
        }
        
        private void AddPlayerToQueue(string playerId)
        {
            if (!PlayerInQueue(playerId))
            {
                Console.WriteLine("AddPlayerToQueue");
                dataService.UnsortedPlayers.TryAdd(playerId, DateTime.UtcNow);    
            }
        }

        void TryCreateRoom()
        {
            Console.WriteLine("TryCreateRoom");
            bool thereIsAFullSetOfPlayers = dataService.UnsortedPlayers.Count >= Globals.NumbersOfPlayersInRoom;
            if ( thereIsAFullSetOfPlayers) CreateRoom(Globals.NumbersOfPlayersInRoom).Wait();
        }

        /// <summary>
        /// Создаёт комнату если есть хотя бы один игрок.
        /// </summary>
        public async Task CreateRoom(int maxNumberOfPlayers)
        {
            //Достать maxNumberOfPlayers игроков
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
                GameServerIp = Globals.DefaultGameServerIp,
                GameServerPort = 48956
            };
            
            //Поставить игрокам статус "В бою"
            AddPlayersToTheListOfPlayersInBattle(playersInfo, newRoomNumber);

            //Положить кооманту в коллекцию
            while (!dataService.GameRoomsData.TryAdd(gameRoomData.GameRoomNumber, gameRoomData))
            {
                
            }

            //Отослать данные на игровой сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(gameRoomData);
        }

        //TODO: возможно, боты вообще должны быть отдельными и, например, без GoogleId
        void AddBotsToList(ref List<PlayerInfoForGameRoom> players, int botsCount)
        {
            Random random = new Random();
            for (int i = 0; i < botsCount; i++)
            {
                var dich = new PlayerInfoForGameRoom
                {
                    //IsBot = true,
                    GoogleId = "Bot_"+ random.Next(1,int.MaxValue),
                    TemporaryId = PlayersTemporaryIdGenerator.GetPlayerId()
                };
                players.Add(dich);
            }
            
        }
        List<PlayerInfoForGameRoom> GetPlayersFromQueue(int numberOfPlayers)
        {
            List<PlayerInfoForGameRoom> playersInfo = new List<PlayerInfoForGameRoom>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                var (playerId, requestTime) = dataService.UnsortedPlayers.LastOrDefault();

                if (playerId != null)
                {
                    if (dataService.UnsortedPlayers.TryRemove(playerId, out var playerRequest))
                    {
                        var dich = new PlayerInfoForGameRoom
                        {
                            GoogleId = playerId,
                            TemporaryId = PlayersTemporaryIdGenerator.GetPlayerId()
                        };
                        playersInfo.Add(dich);
                    }
                    else
                    {
                        throw new Exception("Не удалось извлечь игрока из очереди. Ключ = "+playerId);
                    }   
                }
            }

            return playersInfo;
        }
        private void AddPlayersToTheListOfPlayersInBattle(List<PlayerInfoForGameRoom> players, int gameRoomNumber)
        {
            foreach (var player in players)
            {
                dataService.PlayersInGameRooms.TryAdd(player.GoogleId, gameRoomNumber);
            }
        }
        public bool PlayerInQueue(string playerId)
        {
            Console.WriteLine($"Обработка запроса от игрока. кол-во в очереди {dataService.UnsortedPlayers.Count}. ");
            return dataService.UnsortedPlayers.ContainsKey(playerId);
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
            
            Console.WriteLine("Старт удаления игроков");
            //Удалить всех игроков
            foreach (var player in deletingRoom.Players)
            {
                Console.WriteLine("Удаление игрока с id = "+player.GoogleId);
                if (dataService.PlayersInGameRooms.TryRemove(player.GoogleId, out var roomNum))
                {
                    
                }
                else
                {
                    throw new Exception("Не удалось удалить игрока");
                }
            }
            Console.WriteLine("Старт удаления комнаты");
            //Удалить комнату
            if (dataService.GameRoomsData.TryRemove(roomNumber, out var gameRoomData))
            {
                Console.WriteLine("Комната успешно удалена");
            }
            else
            {
                throw new Exception("Не удалось удалить комнату");
            }
        }

        public int GetNumberOfPlayersInQueue()
        {
            return dataService.UnsortedPlayers.Count;
        }

        public int GetNumberOfPlayersInBattles()
        {
            return dataService.PlayersInGameRooms.Count;
        }

        public bool TryRemovePlayerFromBattle(string playerId)
        {
            if (dataService.PlayersInGameRooms.ContainsKey(playerId))
            {
                if (dataService.PlayersInGameRooms.Remove(playerId, out int roomId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}