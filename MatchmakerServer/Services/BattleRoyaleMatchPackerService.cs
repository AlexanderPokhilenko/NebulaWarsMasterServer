using System.Collections.Generic;
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

        public (bool success, List<PlayerInfo> playersInfo) TryCreateMatch(int maxNumberOfPlayersInBattle, 
            bool botsCanBeUsed)
        {
            //Если мало игроков и нельзя дополнять ботами, то матч собрать не получится
            if (battleRoyaleQueueService.GetNumberOfPlayersInQueue() < Globals.NumbersOfPlayersInBattleRoyaleMatch
                && !botsCanBeUsed)
            {
                return (false, null);
            }

            //Достать игроков из очереди без извлечения
            var playersInfo = battleRoyaleQueueService.TakeHead(maxNumberOfPlayersInBattle);

            //Дополнить ботами, если нужно
            if (playersInfo.Count < Globals.NumbersOfPlayersInBattleRoyaleMatch)
            {
                //Дополнить ботами, если можно
                if (botsCanBeUsed)
                {
                    playersInfo.AddRange(CreateBots(maxNumberOfPlayersInBattle-playersInfo.Count));
                }
            }
        
            //Если игроков достаточно, то был матч может быть запущен
            if (playersInfo.Count == Globals.NumbersOfPlayersInBattleRoyaleMatch)
            {
                return (true, playersInfo);
            }
            //Иначе собрать матч не удалось
            else
            {
                return (false, null);
            }
        }
        
        /// <summary>
        /// Создаёт список ботов для дополнения списка игроков
        /// </summary>
        /// <param name="numberOdBots"></param>
        /// <returns></returns>
        private List<PlayerInfo> CreateBots(int numberOdBots)
        {
            List<PlayerInfo> bots = new List<PlayerInfo>();
            for (int i = 0; i < numberOdBots; i++)
            {
                PlayerInfo bot = new PlayerInfo
                {
                    PlayerId = "Bot_" + PlayersTemporaryIdGenerator.GetPlayerId(),
                    WarshipCopy = new WarshipCopy
                    {
                        PrefabName = "Bird"
                    }
                };
            }
            return bots;
        }
    }
}