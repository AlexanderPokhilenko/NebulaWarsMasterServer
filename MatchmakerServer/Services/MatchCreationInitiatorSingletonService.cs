using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Создаёт бой наполненный ботами, если есть человек, который ждёт в очереди больше максимального времени.
    /// </summary>
    public class MatchCreationInitiatorSingletonService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueService;
        private readonly BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService;

        public MatchCreationInitiatorSingletonService(BattleRoyaleQueueSingletonService battleRoyaleQueueService, 
            BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService)
        {
            this.battleRoyaleQueueService = battleRoyaleQueueService;
            this.battleRoyaleMatchCreatorService = battleRoyaleMatchCreatorService;
        }

        public void StartThread()
        {
            Thread thread = new Thread(PeriodicCreationOfGameRooms);
            thread.Start();
        }
        
        private async void PeriodicCreationOfGameRooms()
        {
            while (true)
            {
                await Task.Delay(1000);
                await TryCreateBattleRoyaleMatch();
            }
            // ReSharper disable once FunctionNeverReturns
        }

        private async Task TryCreateMatch()
        {
            await battleRoyaleMatchCreatorService.TryCreateMatch(Globals.NumbersOfPlayersInRoom);
        }
        
        private async Task TryCreateBattleRoyaleMatch()
        {
            Console.WriteLine("Попытка собрать комнату принудительно.");
            if (IsWaitingTimeExceeded())
            {
                await battleRoyaleMatchCreatorService.CreateWithBots(Globals.NumbersOfPlayersInRoom);
            }
        }

        /// <summary>
        /// Есть ли игрок, который ждёт слишком долго?
        /// </summary>
        /// <returns></returns>
        private bool IsWaitingTimeExceeded()
        {
            DateTime? oldestRequestTime = battleRoyaleQueueService.GetOldestRequestTime();
            if (oldestRequestTime == null)
            {
                return false;
            }
            var deltaTime = DateTime.UtcNow - oldestRequestTime.Value;
            return deltaTime.TotalSeconds > Globals.MaxStandbyTimeSec;
        }
    }
}