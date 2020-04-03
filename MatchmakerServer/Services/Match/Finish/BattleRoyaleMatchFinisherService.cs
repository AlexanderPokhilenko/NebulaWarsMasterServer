using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

//TODO это пиздец

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за дописывания результатов боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchFinisherService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService;

        public BattleRoyaleMatchFinisherService(ApplicationDbContext dbContext,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService)
        {
            this.dbContext = dbContext;
            this.unfinishedMatchesSingletonService = unfinishedMatchesSingletonService;
        }
        
        public async Task PlayerDeath(int accountId, int placeInMatch, int matchId)
        {
            MatchResultForPlayer matchResultForPlayer = await dbContext
                .MatchResultForPlayers
                .SingleOrDefaultAsync(matchResult => 
                    matchResult.MatchId == matchId 
                    && matchResult.AccountId == accountId);

            
            if (matchResultForPlayer == null)
            {
                Console.WriteLine("matchResultForPlayer is null");
                return;
            }

            matchResultForPlayer.PlaceInMatch = placeInMatch;
            matchResultForPlayer.PremiumCurrencyDelta = 0;
            matchResultForPlayer.RegularCurrencyDelta = 10;
            matchResultForPlayer.WarshipRatingDelta = 5;
            matchResultForPlayer.PointsForBigChest = 0;
            matchResultForPlayer.PointsForSmallChest = 2;

            await dbContext.SaveChangesAsync();
            
            //удаление игрока из структуры данных
            //TODO это пиздец
            var account = await dbContext.Accounts.SingleAsync(account1 => account1.Id == accountId);

            bool success = unfinishedMatchesSingletonService.TryRemovePlayerFromMatch(account.ServiceId);
            if (!success)
            {
                Console.WriteLine("Произошла дичь. ");
            }
        }

       
        public async Task DeleteRoom(int matchId)
        {
            await AddResultsOfMatchToDatabase(matchId);
            bool success = unfinishedMatchesSingletonService.TryRemoveMatch(matchId);
            if (!success)
            {
                Console.WriteLine("\nНе удалось удалить матч\n");
            }
        }

        private async Task AddResultsOfMatchToDatabase(int matchId)
        {
            Console.WriteLine($"\n{nameof(AddResultsOfMatchToDatabase)}\n");
            
            //Игроки для которых результаты боя не были записаны
            var matchResultForPlayers = dbContext.MatchResultForPlayers
                .Where(matchResultForPlayer => 
                        matchResultForPlayer.MatchId == matchId
                    && matchResultForPlayer.RegularCurrencyDelta==null)
                .ToList()
                ;

            //TODO заполнить все данные
            for (int i = 0; i < matchResultForPlayers.Count(); i++)
            {
                var matchResultForPlayer = matchResultForPlayers[i];
                Console.WriteLine($"\nЗапись результата матча для игрока {matchResultForPlayer.AccountId}\n");
                int placeInMatch = i + 1;
                
                matchResultForPlayer.PlaceInMatch = placeInMatch;
                matchResultForPlayer.PremiumCurrencyDelta = 0;
                matchResultForPlayer.RegularCurrencyDelta = 10;
                matchResultForPlayer.WarshipRatingDelta = 5;
                matchResultForPlayer.PointsForBigChest = 0;
                matchResultForPlayer.PointsForSmallChest = 2;
            }
            
            //TODO сохранить всё в БД
            await dbContext.SaveChangesAsync();
            Console.WriteLine($"\nМатч {nameof(matchId)} {matchId} был окончен\n");
        }
    }
}