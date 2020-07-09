using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.MatchCreation;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services.MatchCreationInitiation
{
    /// <summary>
    /// Инициирует создание матчей для всех режимов
    /// </summary>
    public class MatchCreationInitiator
    {
        private readonly IPlayerTimeoutManager playerTimeoutManager;
        private readonly BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService;
        private readonly int numberOfPlayers = Globals.NumbersOfPlayersInBattleRoyaleMatch;

        public MatchCreationInitiator(BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService,
            IPlayerTimeoutManager playerTimeoutManager)
        {
            this.battleRoyaleMatchCreatorService = battleRoyaleMatchCreatorService;
            this.playerTimeoutManager = playerTimeoutManager;
        }

        public async Task<bool> TryCreateBattleRoyaleMatch()
        {
            bool matchWasCreated = false;
            
            //Собери бои только из игроков, пока хватает игроков
            while (true)
            {
                bool success = await battleRoyaleMatchCreatorService.TryCreateMatch(numberOfPlayers, false);
                
                if (!success)
                {
                    Console.WriteLine("Не удалось собрать матч");
                    break;
                }
                else
                {
                    matchWasCreated = true;
                }
            }
            
            //Собери бой с ботами, если кто-то долго ждёт
            if (playerTimeoutManager.IsWaitingTimeExceeded())
            {
                bool success = await battleRoyaleMatchCreatorService
                    .TryCreateMatch(numberOfPlayers, true);
                if (!success)
                {
                    Console.WriteLine("Не удалось собрать матч");
                }
                else
                {
                    matchWasCreated = true;
                }
            }

            return matchWasCreated;
        }
    }
}