using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Формирует объект с данными про матч на основе записи из БД
    /// </summary>
    public static class MatchDataFactory
    {
        public static BattleRoyaleMatchData Create(GameUnitsForMatch gameUnitsForMatch, Match match)
        {
            var result = new BattleRoyaleMatchData
            {
                MatchId = match.Id,
                GameServerIp = match.GameServerIp,
                GameServerPort = match.GameServerUdpPort,
                GameUnitsForMatch = new GameUnitsForMatch
                {
                    Bots = gameUnitsForMatch.Bots,
                    Players = gameUnitsForMatch.Players
                }
            };
            return result;
        }
    }
    
     
    /// <summary>
    /// Записывает данные матча в БД
    /// </summary>
    public class MatchDataDbWriterService
    {
        // private readonly ApplicationDbContext dbContext;
        //
        // public MatchDataDbWriterService(ApplicationDbContext dbContext)
        // {
        //     this.dbContext = dbContext;
        // }
        
        /// <summary>
        /// Возвращает id матча после успешной записи в БД
        /// </summary>
        public async Task<Match> WriteMatchDataToDb(MatchRoutingData matchRoutingData, 
            List<PlayerQueueInfo> playersQueueInfo)
        {
            ApplicationDbContext dbContext = DbContextFactory.CreateDbContext();
            //Создать объекты для результатов боя игроков
            var playersResult = new List<PlayerMatchResult>();
            foreach (var playerQueueInfo in playersQueueInfo)
            {
                PlayerMatchResult playerMatchResult = new PlayerMatchResult
                {
                    AccountId = playerQueueInfo.AccountId,
                    WarshipId = playerQueueInfo.Warship.Id
                };
                playersResult.Add(playerMatchResult);
            }

            //Создать матч
            Match match = new Match
            {
                StartTime = DateTime.UtcNow,
                GameServerIp = matchRoutingData.GameServerIp,
                GameServerUdpPort = matchRoutingData.GameServerPort,
                PlayerMatchResults = playersResult
            };

            await dbContext.Matches.AddAsync(match);
            await dbContext.SaveChangesAsync();

            return match;
        }
    }
}