using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services
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
            //Запрос в БД
            var matchResult = await dbContext.MatchResultForPlayers
                // .Include(rec=>rec.Warship)
                //     .ThenInclude(warship => warship.WarshipType)
                // .Include(rec=>rec.Warship)
                //     .ThenInclude(warship => warship.Account)
                .SingleOrDefaultAsync(rec => 
                    rec.MatchId == matchId 
                    && rec.Warship.Account.ServiceId == playerServiceId);
            
            //Такой матч существует?
            if (matchResult == null)
            {
                Console.WriteLine("\n\n\n\n\n\nmatchResult == null");
                return null;
            }
            
            //Результат игрока записан?
            if (matchResult.PlaceInMatch == null
                || matchResult.PremiumCurrencyDelta == null
                || matchResult.RegularCurrencyDelta == null
                || matchResult.WarshipRatingDelta == null
                || matchResult.PointsForBigChest == null
                || matchResult.PointsForSmallChest == null)
            {
                Console.WriteLine("Игрок не закончил этот матч.");
                return null;
            }


            int oldSpaceShipRating = matchResult.Warship.MatchResultForPlayers
                .Where(matchResultForPlayer => matchResultForPlayer.Match.StartTime<matchResult.Match.StartTime)
                .Sum(value=>value.WarshipRatingDelta) ?? 0;
            
            
            var playerAchievements = new MatchResult
            {
                DoubleTokens = false,
                MatchRatingDelta = matchResult.WarshipRatingDelta.Value,
                CurrentSpaceshipRating = oldSpaceShipRating,
                RankingRewardTokens = matchResult.RegularCurrencyDelta.Value,
                SpaceshipPrefabName = matchResult.Warship.WarshipType.Name
            };

            return playerAchievements; 
        }
    }
}