using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за правильное создание боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly BattleRoyaleMatchPackerService battleRoyaleMatchPackerService;
        private ApplicationDbContext dbContext;
        private readonly GameServerNegotiatorService gameServerNegotiatorService;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            ApplicationDbContext dbContext, GameServerNegotiatorService gameServerNegotiatorService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            this.dbContext = dbContext;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
        }

        
        public async Task<bool> TryCreateMatch(int numbersOfPlayersInRoom)
        {
            throw new NotImplementedException();
        }
        
        public async Task<bool> CreateWithBots(int maxNumberOfPlayersInBattle)
        {
            BattleRoyaleMatchData battleRoyaleMatchData = battleRoyaleMatchPackerService
                .CreateMatch(maxNumberOfPlayersInBattle);
            
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
    
    public class BattleRoyaleMatchPackerService
    {
        private BattleRoyaleQueueSingletonService battleRoyaleQueueService;
        
        public BattleRoyaleMatchData CreateMatch(int maxNumberOfPlayersInBattle)
        {
            var playersInfo = battleRoyaleQueueService.GetPlayersFromQueue(maxNumberOfPlayersInBattle);
            
            if (playersInfo.Count == 0)
            {
                throw new Exception("такого не должно происходить");
            }
            
            //Дополнить ботами
            playersInfo.AddRange(CreateBots(maxNumberOfPlayersInBattle-playersInfo.Count));
            
            throw new Exception();
        }
        
        private List<PlayerInfo> CreateBots(int numberOdBots)
        {
            throw new NotImplementedException();
        }
    }
}

