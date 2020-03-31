using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Utils;

namespace AmoebaGameMatcherServer.Services
{
    public interface IPlayerTimeoutManager
    {
        bool IsWaitingTimeExceeded();
    }
    
    public class PlayerTimeoutManagerServiceStub:IPlayerTimeoutManager
    {
        public bool IsWaitingTimeExceeded()
        {
            Console.WriteLine(nameof(IsWaitingTimeExceeded));
            return true;
        }
    }

    /// <summary>
    /// Инициирует создание матчей для всех режимов
    /// </summary>
    public class MatchCreationInitiator
    {
        private readonly BattleRoyaleMatchCreatorService battleRoyaleMatchCreatorService;
        private readonly IPlayerTimeoutManager playerTimeoutManager;
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
            
            //Собирай бои только из игроков, пока можешь
            bool tryMore = true;
            while (tryMore)
            {
                var result = 
                    await battleRoyaleMatchCreatorService.TryCreateMatch(numberOfPlayers, false);
                tryMore = result.Success;
                if (!result.Success)
                {
                    Console.WriteLine("Не удалось собрать матч по причине "+result.FailureReason);
                }
                else
                {
                    matchWasCreated = true;
                }
            }
            
            //Собери бой с ботами, если кто-то долго ждёт
            if (playerTimeoutManager.IsWaitingTimeExceeded())
            {
                var result = 
                    await battleRoyaleMatchCreatorService.TryCreateMatch(numberOfPlayers, true);
                if (!result.Success)
                {
                    Console.WriteLine("Не удалось собрать матч по причине "+result.FailureReason);
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