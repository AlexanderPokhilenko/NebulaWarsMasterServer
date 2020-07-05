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
        private readonly WarshipRatingReaderService warshipRatingReaderService;

        public PlayerMatchResultDbReaderService(ApplicationDbContext dbContext, 
            WarshipRatingReaderService warshipRatingReaderService)
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
            var increments = matchResult.Transaction.Increments;
            
            if (increments.Count == 0)
            {
                throw new Exception("Игрок ничего не заработал за бой");
            }
            
            foreach (var increment in increments)
            {
                Console.WriteLine(increment.IncrementTypeId+" "+increment.MatchRewardTypeId);
                if (increment.IncrementTypeId == IncrementTypeEnum.LootboxPoints)
                {
                    Console.WriteLine("это лутбокс");
                    if (increment.MatchRewardTypeId!=null)
                    {
                        Console.WriteLine("добавление");
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
                WarshipPrefabName = matchResult.Warship.WarshipType.Name
            };
            
            return matchResultDto; 
        }
    }
}