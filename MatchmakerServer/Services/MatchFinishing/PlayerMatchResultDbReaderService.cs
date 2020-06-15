using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
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
        private readonly WarshipReaderService warshipReaderService;

        public PlayerMatchResultDbReaderService(ApplicationDbContext dbContext, 
            WarshipReaderService warshipReaderService)
        {
            this.dbContext = dbContext;
            this.warshipReaderService = warshipReaderService;
        }

        public async Task<MatchResult> ReadMatchResultAsync(int matchId, string playerServiceId)
        {
            MatchResultForPlayer matchResultDb = await dbContext.MatchResultForPlayers
                .Include(matchResult1=>matchResult1.Warship)
                    .ThenInclude(warship => warship.WarshipType)
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
            if (!matchResultDb.IsFinished)
            {
                Console.WriteLine("Игрок не закончил этот матч.");
                return null;
            }


            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(matchResultDb.WarshipId);
            
            
            MatchResult matchResult = new MatchResult();
            matchResult.CurrentSpaceshipRating = currentWarshipRating;
            matchResult.MatchRatingDelta = matchResultDb.WarshipRatingDelta;
            matchResult.PointsForSmallChest = matchResultDb.SmallLootboxPoints;
            matchResult.DoubleTokens = false;
            matchResult.SpaceshipPrefabName = matchResultDb.Warship.WarshipType.Name;

            return matchResult; 
        }
    }
}