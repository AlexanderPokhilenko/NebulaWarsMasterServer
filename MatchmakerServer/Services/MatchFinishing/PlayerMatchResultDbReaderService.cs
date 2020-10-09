using DataLayer;
using DataLayer.Tables;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatchResult = DataLayer.Tables.MatchResult;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    public class PlayerMatchResultDbReaderService : IPlayerMatchResultDbReaderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWarshipRatingReaderService warshipRatingReaderService;

        public PlayerMatchResultDbReaderService(ApplicationDbContext dbContext, 
            IWarshipRatingReaderService warshipRatingReaderService)
        {
            this.dbContext = dbContext;
            this.warshipRatingReaderService = warshipRatingReaderService;
        }

        public async Task<MatchResultDto> ReadMatchResultAsync(int matchId, string playerServiceId)
        {
            MatchResult matchResult = await dbContext.MatchResults
                .Include(matchResult1=>matchResult1.Warship)
                    .ThenInclude(warship => warship.WarshipType)
                .Include(matchResult1 => matchResult1.Transaction)
                    .ThenInclude(resource => resource.Increments)
                .Include(matchResult1 => matchResult1.Transaction)
                    .ThenInclude(resource => resource.Decrements)
                .SingleOrDefaultAsync(matchResult1 => 
                    matchResult1.MatchId == matchId 
                    && matchResult1.Warship.Account.ServiceId == playerServiceId);
            
            //Такой матч существует?
            if (matchResult == null)
            {
                throw new NullReferenceException(nameof(matchResult));
                return null;
            }
            
            //Результат игрока записан?
            if (!matchResult.IsFinished)
            {
                throw new Exception("Игрок не закончил этот матч");
                return null;
            }
            
            int currentWarshipRating = await warshipRatingReaderService.ReadWarshipRatingAsync(matchResult.WarshipId);
            var lootboxPoints = new Dictionary<MatchRewardTypeEnum, int>();
            
            
            if (matchResult.Transaction.Increments.Count == 0)
            {
                throw new Exception("Игрок ничего не заработал за бой");
            }
            
            foreach (Increment increment in matchResult.Transaction.Increments)
            {
                if (increment.IncrementTypeId == IncrementTypeEnum.LootboxPoints)
                {
                    if (increment.MatchRewardTypeId != null)
                    {
                        lootboxPoints.Add(increment.MatchRewardTypeId.Value, increment.Amount);
                    }
                }
            }

            int warshipRatingIncrement = matchResult.Transaction.Increments
                .Where(increment => increment.IncrementTypeId == IncrementTypeEnum.WarshipRating)
                .Sum(increment => increment.Amount);
            int warshipRatingDecrement = matchResult.Transaction.Decrements
                .Where(decrement => decrement.DecrementTypeId == DecrementTypeEnum.WarshipRating)
                .Sum(decrement => decrement.Amount);
            int matchRatingDelta = warshipRatingIncrement - warshipRatingDecrement;

            MatchResultDto matchResultDto = new MatchResultDto
            {
                CurrentWarshipRating = currentWarshipRating,
                MatchRatingDelta = matchRatingDelta,
                LootboxPoints = lootboxPoints,
                SkinName = matchResult.Warship.WarshipType.Name
            };
            
            return matchResultDto; 
        }
    }
}