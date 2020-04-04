using System;
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

        public async Task<PlayerAchievements> GetMatchResult(int matchId, string playerServiceId)
        {
            //Запрос в БД
            var matchResult = await dbContext.MatchResultForPlayers
                .Include(rec=>rec.Warship)
                    .ThenInclude(warship => warship.WarshipType)
                .Include(rec=>rec.Account)
                .SingleOrDefaultAsync(rec => 
                    rec.MatchId == matchId 
                    && rec.Account.ServiceId == playerServiceId);
            
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

            var playerAchievements = new PlayerAchievements
            {
                DoubleTokens = false,
                BattleRatingDelta = matchResult.WarshipRatingDelta.Value,
                OldSpaceshipRating = matchResult.Warship.Rating - matchResult.WarshipRatingDelta.Value,
                RankingRewardTokens = matchResult.RegularCurrencyDelta.Value,
                SpaceshipPrefabName = matchResult.Warship.WarshipType.Name
            };

            return playerAchievements; 
        }
    }
}