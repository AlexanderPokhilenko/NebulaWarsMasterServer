using System;
using AmoebaGameMatcherServer.Services.Queues;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services.MatchCreationInitiation
{
     
    public class PlayerTimeoutManagerService:IPlayerTimeoutManager
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueService;

        public PlayerTimeoutManagerService(BattleRoyaleQueueSingletonService battleRoyaleQueueService)
        {
            this.battleRoyaleQueueService = battleRoyaleQueueService;
        }

        /// <summary>
        /// Есть ли игрок, который ждёт слишком долго?
        /// </summary>
        /// <returns></returns>
        public bool IsWaitingTimeExceeded()
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