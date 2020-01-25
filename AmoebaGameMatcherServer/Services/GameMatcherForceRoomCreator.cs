using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;

//TODO async void что с ним делать?
//TODO говнокод
//TODO дублирование кода

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherForceRoomCreator
    {
        private readonly GameMatcherDataService dataService;
        private GameMatcherService gameMatcherService;
        
        public GameMatcherForceRoomCreator(GameMatcherDataService dataService, GameMatcherService gameMatcherService)
        {
            this.dataService = dataService;
            this.gameMatcherService = gameMatcherService;
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
            Console.WriteLine("Попытка собрать комнату принудительно.");

            if (dataService.UnsortedPlayers.TryPeek(out var oldestRequest))
            {
                Console.WriteLine($"oldestRequest.Time = {oldestRequest.Time}");
                var deltaTime = DateTime.UtcNow - oldestRequest.Time;
                if (deltaTime.TotalSeconds > Globals.maxStandbyTimeSec)
                {
                    //есть человек, который долго ждёт
                    gameMatcherService.CreateRoom(Globals.NumbersOfPlayersInRoom).Wait();
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

        
    }
}