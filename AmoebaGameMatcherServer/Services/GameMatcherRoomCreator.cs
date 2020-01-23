using System;
using System.Threading;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherRoomCreator
    {
        private readonly GameMatcherDataService dataService;

        public GameMatcherRoomCreator(GameMatcherDataService dataService)
        {
            this.dataService = dataService;
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
            if (dataService.unsortedPlayers.TryPeek(out var oldestRequest))
            {
                var deltaTime = DateTime.UtcNow - oldestRequest.Time;
                if (deltaTime.TotalSeconds > 20)
                {
                    //создать комнату с ботами
                }
                else
                {
                    //самое большое время ожидания в очереди меньше 20 сек
                }
            }
            else
            {
                //коллекция пуста
            }
        }
    }
}