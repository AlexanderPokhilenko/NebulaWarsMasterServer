using System;
using System.Threading;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Инициирует создание матчей для всех режимов
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
            Thread thread = new Thread(PeriodicDich);
            thread.Start();
        }
        
        private async void PeriodicDich()
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
            await battleRoyaleMatchCreatorService.TryCreateMatch(Globals.NumbersOfPlayersInBattleRoyaleMatch);
        }
        
        private async Task TryCreateBattleRoyaleMatch()
        {
            Console.WriteLine("Попытка собрать комнату принудительно.");
            if (IsWaitingTimeExceeded())
            {
                await battleRoyaleMatchCreatorService.CreateWithBots(Globals.NumbersOfPlayersInBattleRoyaleMatch);
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