using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherForceRoomCreator
    {
        private readonly GameMatcherDataService dataService;
        private readonly GameMatcherService gameMatcherService;
        
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
            // ReSharper disable once FunctionNeverReturns
        }

        private void TryCreateRoom()
        {
            Console.WriteLine("Попытка собрать комнату принудительно.");
            if (dataService.UnsortedPlayers.Count > 0)
            {
                DateTime oldestRequestTime = dataService.UnsortedPlayers.Min(r => r.Value);
           
                Console.WriteLine($"oldestRequest.Time = {oldestRequestTime}");
                var deltaTime = DateTime.UtcNow - oldestRequestTime;
                if (deltaTime.TotalSeconds > Globals.MaxStandbyTimeSec)
                {
                    //есть человек, который долго ждёт
                    gameMatcherService.CreateRoom(Globals.NumbersOfPlayersInRoom).Wait();
                }
            }
        }
    }
}