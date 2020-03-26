using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Полностью управляет созданием боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly BattleRoyaleMatchPackerService battleRoyaleMatchPackerService;
        private readonly IGameServerNegotiatorService gameServerNegotiatorService;
        private readonly MatchmakerDichService matchmakerDichService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;
        private readonly ApplicationDbContext dbContext;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            ApplicationDbContext dbContext, IGameServerNegotiatorService gameServerNegotiatorService,
            MatchmakerDichService matchmakerDichService, 
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            this.dbContext = dbContext;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
            this.matchmakerDichService = matchmakerDichService;
            this.unfinishedMatchesService = unfinishedMatchesService;
        }
        
        public async Task<(bool success, MatchCreationFailureReason? failureReason)> 
            TryCreateMatch(int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            //Попробовать достать игроков из очереди
            var (success, playersInfo) = battleRoyaleMatchPackerService
                .GetPLayersForMatch(maxNumberOfPlayersInBattle, botsCanBeUsed);

            //Достаточно игроков?
            if (!success)
            {
                return (false, MatchCreationFailureReason.NotEnoughPlayers);
            }

            //На каком сервере будет запучаться матч?
            var matchRoutingData = matchmakerDichService.GetMatchRoutingData();

            //перенести игроков в очередь ожидания            
            
            
            //Сделать запись об матче в БД
            BattleRoyaleMatchData matchData = await WriteMatchDataToDb(matchRoutingData, playersInfo);

            //Добавить игроков в таблицу тех кто в бою
            
            //Сообщить на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(null);
            
            return (true, null);
        }

        private async Task<BattleRoyaleMatchData> WriteMatchDataToDb(MatchRoutingData matchRoutingData, 
            List<PlayerInfo> playersInfo)
        {
            var playersResult = new List<PlayerMatchResult>();
            foreach (var player in playersInfo)
            {
                PlayerMatchResult playerMatchResult = new PlayerMatchResult
                {
                    AccountId = player.AccountId,
                    WarshipId = player.WarshipCopy.Id
                };
                playersResult.Add(playerMatchResult);
            }

            Match match = new Match
            {
                StartTime = DateTime.UtcNow,
                GameServerIp = matchRoutingData.GameServerIp,
                GameServerUdpPort = matchRoutingData.GameServerPort,
                PlayerMatchResults = playersResult
            };

            await dbContext.Matches.AddAsync(match);
            await dbContext.SaveChangesAsync();

            BattleRoyaleMatchData battleRoyaleMatchData = new BattleRoyaleMatchData()
            {
                Players = playersInfo,
                MatchId = match.Id,
                GameServerIp = matchRoutingData.GameServerIp,
                GameServerPort = matchRoutingData.GameServerPort
            };

            return battleRoyaleMatchData;
        }
    }

    public enum MatchCreationFailureReason
    {
        NotEnoughPlayers
    }
}

