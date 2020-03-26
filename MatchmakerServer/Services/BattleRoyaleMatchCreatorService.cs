using System;
using System.Threading.Tasks;
using DataLayer;

namespace AmoebaGameMatcherServer.Services
{
    public class GameServerData
    {
        public string GameServerIp;
        public int GameServerPort;
    }

    /// <summary>
    /// Полностью управляет созданием боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly BattleRoyaleMatchPackerService battleRoyaleMatchPackerService;
        private readonly GameServerNegotiatorService gameServerNegotiatorService;
        private ApplicationDbContext dbContext;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            ApplicationDbContext dbContext, GameServerNegotiatorService gameServerNegotiatorService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            this.dbContext = dbContext;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
        }
        
        public async Task<bool> TryCreateMatch(int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            var (success, playersInfo) = battleRoyaleMatchPackerService
                .GetPLayersForMatch(maxNumberOfPlayersInBattle, botsCanBeUsed);

            if (!success)
            {
                //Не удалось набрать достаточно игроков
                return false;
            }
            
            //TODO перенести игроков в список тех, кто в бою
            await WriteMatchDataToDb(battleRoyaleMatchData);
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(null);
            return true;
        }

        private async Task WriteMatchDataToDb(object matchData)
        {
            throw new NotImplementedException();
        }
    }
}

