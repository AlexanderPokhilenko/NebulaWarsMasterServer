using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;
using MatchResult = DataLayer.Tables.MatchResult;

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

        public async Task<MatchResultDto> ReadMatchResultAsync(int matchId, string playerServiceId)
        {
            MatchResult matchResult = await dbContext.MatchResults
                .Include(matchResult1=>matchResult1.Warship)
                    .ThenInclude(warship => warship.WarshipType)
                .Include(matchResult1 => matchResult1.Transaction)
                    .ThenInclude(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Increments)
                .Include(matchResult1 => matchResult1.Transaction)
                    .ThenInclude(transaction => transaction.Resources)
                        .ThenInclude(resource => resource.Decrements)
                .SingleOrDefaultAsync(matchResult1 => 
                    matchResult1.MatchId == matchId 
                    && matchResult1.Warship.Account.ServiceId == playerServiceId);
            
            //Такой матч существует?
            if (matchResult == null)
            {
                Console.WriteLine("\n\n\n\n\n\nmatchResult == null");
                return null;
            }
            
            //Результат игрока записан?
            if (!matchResult.IsFinished)
            {
                Console.WriteLine("Игрок не закончил этот матч.");
                return null;
            }
            
            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(matchResult.WarshipId);
            var lootboxPoints = new Dictionary<MatchRewardTypeEnum, int>();
            foreach (var increment in matchResult.Transaction.Resources.SelectMany(resource =>resource.Increments))
            {
                if (increment.IncrementTypeId == IncrementTypeEnum.Lootbox && increment.MatchRewardTypeId!=null)
                {
                    lootboxPoints.Add(increment.MatchRewardTypeId.Value, increment.LootboxPoints);
                }
            }

            int warshipRatingIncrement = matchResult.Transaction.Resources
                .SelectMany(resource => resource.Increments)
                .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                .Sum(increment => increment.WarshipRating);
            int warshipRatingDecrement = matchResult.Transaction.Resources
                .SelectMany(resource => resource.Decrements)
                .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.WarshipRating)
                .Sum(decrement => decrement.WarshipRating);
            int matchRatingDelta = warshipRatingIncrement - warshipRatingDecrement;

            MatchResultDto matchResultDto = new MatchResultDto
            {
                CurrentWarshipRating = currentWarshipRating,
                MatchRatingDelta = matchRatingDelta,
                LootboxPoints = lootboxPoints,
                WarshipPrefabName = matchResult.Warship.WarshipType.Name
            };
            return matchResultDto; 
        }
    }
}