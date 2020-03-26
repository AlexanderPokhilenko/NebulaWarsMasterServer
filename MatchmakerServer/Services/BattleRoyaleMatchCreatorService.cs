using System;
using System.Threading.Tasks;
using DataLayer;

namespace AmoebaGameMatcherServer.Services
{
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
                .TryCreateMatch(maxNumberOfPlayersInBattle,botsCanBeUsed);

            if (!success)
            {
                //Не удалось набрать достаточно игроков
                return false;
            }
            
            await WriteMatchDataToDb(battleRoyaleMatchData);
            //TODO удалить игроков из очереди
            //TODO отправить запрос на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(null);
            throw new NotImplementedException();
        }

        private async Task WriteMatchDataToDb(object matchData)
        {
            throw new NotImplementedException();
        }
    }
}

