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
            BattleRoyaleMatchResult battleRoyaleMatchResultDb = await dbContext.BattleRoyaleMatchResults
                .Include(matchResult1=>matchResult1.Warship)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(rec => 
                    rec.MatchId == matchId 
                    && rec.Warship.Account.ServiceId == playerServiceId);
            
            //Такой матч существует?
            if (battleRoyaleMatchResultDb == null)
            {
                Console.WriteLine("\n\n\n\n\n\nmatchResult == null");
                return null;
            }
            
            //Результат игрока записан?
            if (!battleRoyaleMatchResultDb.IsFinished)
            {
                Console.WriteLine("Игрок не закончил этот матч.");
                return null;
            }


            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(battleRoyaleMatchResultDb.WarshipId);
            
            
            MatchResult matchResult = new MatchResult();
            matchResult.CurrentSpaceshipRating = currentWarshipRating;
            // matchResult.MatchRatingDelta = battleRoyaleMatchResultDb.WarshipRatingDelta;
            // matchResult.PointsForSmallChest = battleRoyaleMatchResultDb.SmallLootboxPoints;
            matchResult.DoubleTokens = false;
            matchResult.SpaceshipPrefabName = battleRoyaleMatchResultDb.Warship.WarshipType.Name;

            return matchResult; 
        }
    }
}