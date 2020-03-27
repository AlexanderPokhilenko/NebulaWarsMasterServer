using System;

namespace AmoebaGameMatcherServer.Services
{
    public class MatchCreationInitiatorSingletonService : MatchCreationInitiator
    {
        private readonly PeriodicTaskExecutor periodicTaskExecutor;

        public void StartThread()
        {
            periodicTaskExecutor.StartThread();
        }

        public MatchCreationInitiatorSingletonService(BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService,
            IPlayerTimeoutManager playerTimeoutManager) : base(battleRoyaleMatchCreatorService, playerTimeoutManager)
        {
            TimeSpan delay = TimeSpan.FromSeconds(1);
            periodicTaskExecutor = new PeriodicTaskExecutor(TryCreateBattleRoyaleMatch, delay);
        }
    }
}