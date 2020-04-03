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
    public class PlayersAchievementsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DoubleTokensManagerService doubleTokensManagerService;

        public PlayersAchievementsService(ApplicationDbContext dbContext, 
            DoubleTokensManagerService doubleTokensManagerService)
        {
            this.dbContext = dbContext;
            this.doubleTokensManagerService = doubleTokensManagerService;
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
                Console.WriteLine("matchResult == null");
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
                DoubleTokens = doubleTokensManagerService.IsDoubleTokensEnabled(0,0),
                BattleRatingDelta = matchResult.WarshipRatingDelta.Value,
                OldSpaceshipRating = 100,
                RankingRewardTokens = matchResult.RegularCurrencyDelta.Value,
                SpaceshipPrefabName = matchResult.Warship.WarshipType.Name
            };

            return playerAchievements; 
        }
    }
}