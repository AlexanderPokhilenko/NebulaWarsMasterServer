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
                await TryCreateRoom();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private async Task TryCreateRoom()
        {
            Console.WriteLine("Попытка собрать комнату принудительно.");
            if (dataService.UnsortedPlayers.Count > 0)
            {
                DateTime oldestRequestTime = dataService.UnsortedPlayers
                    .Select(pair => pair.Value.DictionaryEntryTime)
                    .Min(dateTime => dateTime);
                
                Console.WriteLine($"oldestRequest.Time = {oldestRequestTime}");
                var deltaTime = DateTime.UtcNow - oldestRequestTime;
                if (deltaTime.TotalSeconds > Globals.MaxStandbyTimeSec)
                {
                    //есть человек, который долго ждёт
                    await gameMatcherService.CreateRoomAsync(Globals.NumbersOfPlayersInRoom);
                }
            }
        }
    }
}