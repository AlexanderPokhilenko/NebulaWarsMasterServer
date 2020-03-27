using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public struct MatchCreationMessage
    {
        public bool Success;
        public MatchCreationFailureReason? FailureReason;
        public int? MatchId;
    }
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
        private readonly QueueHelperSukaService sukaService;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            ApplicationDbContext dbContext, IGameServerNegotiatorService gameServerNegotiatorService,
            MatchmakerDichService matchmakerDichService, 
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService, QueueHelperSukaService sukaService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            this.dbContext = dbContext;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
            this.matchmakerDichService = matchmakerDichService;
            this.unfinishedMatchesService = unfinishedMatchesService;
            this.sukaService = sukaService;
        }
        
        public async Task<MatchCreationMessage> 
            TryCreateMatch(int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            //Попробовать достать игроков из очереди
            var (success, playersInfo) = battleRoyaleMatchPackerService
                .GetPlayersForMatch(maxNumberOfPlayersInBattle, botsCanBeUsed);

            //Достаточно игроков?
            if (!success)
            {
                return new MatchCreationMessage
                {
                    Success = false,
                    FailureReason = MatchCreationFailureReason.NotEnoughPlayers,
                    MatchId = null
                };
            }

            //На каком сервере будет запучаться матч?
            var matchRoutingData = matchmakerDichService.GetMatchRoutingData();

            //Сделать запись об матче в БД
            BattleRoyaleMatchData matchData = await WriteMatchDataToDb(matchRoutingData, playersInfo);
            
            //Добавить игроков в таблицу тех кто в бою
            unfinishedMatchesService.AddPlayersToMatch(matchData);
            
            //Извлечь игроков из очереди
            sukaService.RemovePlayersFromQueue(matchData.Players);
            
            //Сообщить на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(matchData);
            
            return new MatchCreationMessage
            {
                Success = true,
                FailureReason = null,
                MatchId = matchData.MatchId
            };
        }

        private async Task<BattleRoyaleMatchData> WriteMatchDataToDb(MatchRoutingData matchRoutingData, 
            List<PlayerQueueInfo> playersInfo)
        {
            var playersResult = new List<PlayerMatchResult>();
            foreach (var playerQueueInfo in playersInfo)
            {
                PlayerMatchResult playerMatchResult = new PlayerMatchResult
                {
                    AccountId = playerQueueInfo.AccountId,
                    WarshipId = playerQueueInfo.Warship.Id
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
                Players = playersInfo.Select((info, i) => info.ToMatchInfo()).ToList(),
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

