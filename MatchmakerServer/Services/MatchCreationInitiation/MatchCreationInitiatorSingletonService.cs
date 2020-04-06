using System;
using AmoebaGameMatcherServer.Services.MatchCreation;

namespace AmoebaGameMatcherServer.Services.MatchCreationInitiation
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