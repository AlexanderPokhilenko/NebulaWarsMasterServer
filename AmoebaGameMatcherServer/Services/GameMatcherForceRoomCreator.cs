using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;

//TODO говнокод


namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherForceRoomCreator
    {
        private readonly GameMatcherDataService dataService;

        public GameMatcherForceRoomCreator(GameMatcherDataService dataService)
        {
            this.dataService = dataService;
        }

        public void StartPeriodicCreationInAnotherThread()
        {
            Thread thread = new Thread(PeriodicCreationOfGameRooms);
            thread.Start();
        }
        private async void PeriodicCreationOfGameRooms()
        {
            while (true)
            {
                await Task.Delay(1000);
                TryCreateRoom();
            }
        }

        private void TryCreateRoom()
        {
            Console.WriteLine($"Попытка собрать комнату насильно. Количество игроков в очереди = " +
                              $"{dataService.UnsortedPlayers.Count}. id data = {dataService.Id}");
            
            
            if (dataService.UnsortedPlayers.TryPeek(out var oldestRequest))
            {
                Console.WriteLine($"oldestRequest.Time = {oldestRequest.Time}");
                var deltaTime = DateTime.UtcNow - oldestRequest.Time;
                if (deltaTime.TotalSeconds > Globals.maxStandbyTimeSec)
                {
                    //есть человек, который долго ждёт
                    ForceCreateRoom();
                }
                else
                {
                    //самое большое время ожидания в очереди меньше n сек
                }
            }
            else
            {
                //коллекция пуста
            }
        }

        /// <summary>
        /// Достаёт всех игроков из очереди в комнату. Если игроков меньше n, то дополняет ботами.
        /// </summary>
        private void ForceCreateRoom()
        {
            //Достать n игроков
            var playersInfo = GetPlayersFromQueue(Globals.NumbersOfPlayersInRoom);
            if(playersInfo.Count==0)
                throw new Exception("При принуждённой сборке комнаты не удалось найти свободных игроков");

            int botsCount = Globals.NumbersOfPlayersInRoom - playersInfo.Count;
            
            //Дополнить остаток ботами
            AddBotsToList(ref playersInfo, botsCount);
            int newRoomNumber = GameRoomIdGenerator.CreateGameRoomNumber();
            
            //Создать комнату
            AddPlayersToTheListOfPlayersInBattle(playersInfo, newRoomNumber);
            GameRoomData gameRoomData = new GameRoomData
            {
                Players = playersInfo.ToArray(),
                GameRoomNumber = newRoomNumber,
                GameServerIp = "localhost:48956/"
            };
            dataService.GameRoomsData.TryAdd(gameRoomData.GameRoomNumber, gameRoomData);
        }

        void AddBotsToList(ref List<PlayerInfoForGameRoom> players, int botsCount)
        {
            Random random = new Random();
            for (int i = 0; i < botsCount; i++)
            {
                var dich = new PlayerInfoForGameRoom
                {
                    IsBot = true,
                    PlayerLogin = "Рандомное имя для бота "+ random.Next(1,Int32.MaxValue) 
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
                else
                {
                    break;
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
    }
}