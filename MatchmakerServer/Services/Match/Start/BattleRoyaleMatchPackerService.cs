using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Utils;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
     
    /// <summary>
    /// Отвечает за доставание набора игроков для матча.
    /// Есть возможность дополнять игроков ботами.
    /// </summary>
    public class BattleRoyaleMatchPackerService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueService;

        public BattleRoyaleMatchPackerService(BattleRoyaleQueueSingletonService battleRoyaleQueueService)
        {
            this.battleRoyaleQueueService = battleRoyaleQueueService;
        }
        
        public (bool success, GameUnitsForMatch, List<QueueInfoForPlayer> playersQueueInfo) GetPlayersForMatch(
            int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            //Если мало игроков и нельзя дополнять ботами, то матч собрать не получится
            if (battleRoyaleQueueService.GetNumberOfPlayersInQueue() < Globals.NumbersOfPlayersInBattleRoyaleMatch
                && !botsCanBeUsed)
            {
                return (false, null, null);
            }

            GameUnitsForMatch gameUnitsForMatch = new GameUnitsForMatch();
            
            //Достать игроков из очереди без извлечения
            List<QueueInfoForPlayer> playersQueueInfo = 
                battleRoyaleQueueService.GetPlayersQueueInfo(maxNumberOfPlayersInBattle);
            gameUnitsForMatch.Players = playersQueueInfo.Select(info => info.ToMatchInfo()).ToList();
            
            //Дополнить ботами, если нужно
            if (gameUnitsForMatch.Players.Count < Globals.NumbersOfPlayersInBattleRoyaleMatch)
            {
                //Дополнить ботами, если можно
                if (botsCanBeUsed)
                {
                    int countOfBots = maxNumberOfPlayersInBattle - gameUnitsForMatch.Players.Count;
                    gameUnitsForMatch.Bots = CreateBots(countOfBots);
                }
            }
        
            
            //Если игроков достаточно, то матч может быть запущен
            if (gameUnitsForMatch.Count() == Globals.NumbersOfPlayersInBattleRoyaleMatch)
            {
                return (true, gameUnitsForMatch, playersQueueInfo);
            }
            //Иначе собрать матч не удалось
            else
            {
                return (false, null, null);
            }
        }
        
        /// <summary>
        /// Создаёт список ботов для дополнения списка игроков
        /// </summary>
        /// <param name="numberOdBots"></param>
        /// <returns></returns>
        private List<BotInfo> CreateBots(int numberOdBots)
        {
            List<BotInfo> bots = new List<BotInfo>();
            for (int i = 0; i < numberOdBots; i++)
            {
                BotInfo botInfo = new BotInfo()
                {
                    IsBot = true,
                    BotName = "Игорь",
                    PrefabName = "Bird",
                    TemporaryId = StubTmpIdGenerator.CreateDich(), //TODO suka
                    WarshipCombatPowerLevel = 1
                };
                bots.Add(botInfo);
            }
            
            return bots;
        }
    }

    public static class StubTmpIdGenerator
    {
        private static int lastId=0;
        public static int CreateDich()
        {
            lastId++;
            return lastId;
        }
    }
}