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
        private readonly int numberOfPlayers = Globals.NumbersOfPlayersInBattleRoyaleMatch;
        
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

        private async Task TryCreateBattleRoyaleMatch()
        {
            //Собирай бои только из игроков, пока можешь
            bool tryMore = true;
            while (tryMore)
            {
                var result = 
                    await battleRoyaleMatchCreatorService.TryCreateMatch(numberOfPlayers, false);
                tryMore = result.Success;
                if (!result.Success)
                {
                    Console.WriteLine("Не удалось созбать матч по причине "+result.FailureReason);
                }
            }
            
            //Собери бой с ботами, если кто-то долго ждёт
            if (IsWaitingTimeExceeded())
            {
                await battleRoyaleMatchCreatorService.TryCreateMatch(numberOfPlayers, true);
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