using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    /// <summary>
    /// Достаёт из БД данные о конкретном бое для аккаунта.
    /// </summary>
    public class PlayerMatchResultDbReaderService
    {
        private readonly ApplicationDbContext dbContext;
        
        public PlayerMatchResultDbReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MatchResult> GetMatchResult(int matchId, string playerServiceId)
        {
            var matchResultDb = await dbContext.MatchResultForPlayers
                .SingleOrDefaultAsync(rec => 
                    rec.MatchId == matchId 
                    && rec.Warship.Account.ServiceId == playerServiceId);
            
            //Такой матч существует?
            if (matchResultDb == null)
            {
                Console.WriteLine("\n\n\n\n\n\nmatchResult == null");
                return null;
            }
            
            //Результат игрока записан?
            if (matchResultDb.PlaceInMatch == null
                || matchResultDb.PremiumCurrencyDelta == null
                || matchResultDb.RegularCurrencyDelta == null
                || matchResultDb.WarshipRatingDelta == null
                || matchResultDb.PointsForBigChest == null
                || matchResultDb.PointsForSmallChest == null)
            {
                Console.WriteLine("Игрок не закончил этот матч.");
                return null;
            }


            int currentWarshipRating = matchResultDb.Warship.MatchResultForPlayers
                .Sum(value=>value.WarshipRatingDelta) ?? 0;
            
            
            var matchResult = new MatchResult
            {
                DoubleTokens = false,
                MatchRatingDelta = matchResultDb.WarshipRatingDelta.Value,
                CurrentSpaceshipRating = currentWarshipRating,
                RankingRewardTokens = matchResultDb.RegularCurrencyDelta.Value,
                SpaceshipPrefabName = matchResultDb.Warship.WarshipType.Name
            };

            return matchResult; 
        }
    }
}