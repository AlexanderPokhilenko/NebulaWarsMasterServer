using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class QueueHelperSukaService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonService;

        public QueueHelperSukaService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonService)
        {
            this.battleRoyaleQueueSingletonService = battleRoyaleQueueSingletonService;
        }

        public void RemovePlayersFromQueue(List<PlayerInfoForMatch> sukaList)
        {
            foreach (var sukaInfo in sukaList)
            {
                battleRoyaleQueueSingletonService.TryRemovePlayerFromQueue(sukaInfo.ServiceId);
            }
        }
    }
}