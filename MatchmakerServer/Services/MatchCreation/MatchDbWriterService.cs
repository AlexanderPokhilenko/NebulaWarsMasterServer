using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    public class MatchDbWriterService
    {
        private readonly IDbContextFactory dbContextFactory;

        public MatchDbWriterService(IDbContextFactory dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }
        
        /// <summary>
        /// Возвращает id матча после успешной записи в БД
        /// </summary>
        public async Task<Match> WriteAsync(MatchRoutingData matchRoutingData, List<int> warshipIds)
        {
            using (ApplicationDbContext dbContext = dbContextFactory.Create())
            {
                //Создать объекты для результатов боя игроков
                List<MatchResult> matchResults = new List<MatchResult>();
                foreach (int warshipId in warshipIds)
                {
                    MatchResult matchResult = new MatchResult
                    {
                        WarshipId = warshipId,
                        IsFinished = false
                    };
                
                    Console.WriteLine($"{nameof(matchResult.WarshipId)} {matchResult.WarshipId}");
                    matchResults.Add(matchResult);
                }

                //Создать матч
                Match match = new Match
                {
                    StartTime = DateTime.UtcNow,
                    GameServerIp = matchRoutingData.GameServerIp,
                    GameServerUdpPort = matchRoutingData.GameServerPort,
                    MatchResults = matchResults,
                    GameModeId = GameModeEnum.BattleRoyale
                };

                await dbContext.Matches.AddAsync(match);
                await dbContext.SaveChangesAsync();
                return match;
            }
        }
    }
}