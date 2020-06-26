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
                throw new NullReferenceException(nameof(matchResult));
                return null;
            }
            
            //Результат игрока записан?
            if (!matchResult.IsFinished)
            {
                throw new Exception("Игрок не закончил этот матч");
                return null;
            }
            
            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(matchResult.WarshipId);
            var lootboxPoints = new Dictionary<MatchRewardTypeEnum, int>();
            var matchIncrements = matchResult.Transaction.Resources
                .SelectMany(resource => resource.Increments);
            var increments = matchIncrements as Increment[] ?? matchIncrements.ToArray();
            if (increments.Length == 0)
            {
                throw new Exception("Игрок ничего не заработал за бой");
            }
            
            foreach (var increment in increments)
            {
                Console.WriteLine(increment.IncrementTypeId.ToString()+" "+increment.MatchRewardTypeId.Value.ToString());
                if (increment.IncrementTypeId == IncrementTypeEnum.Lootbox)
                {
                    Console.WriteLine("это лутбокс");
                    if (increment.MatchRewardTypeId!=null)
                    {
                        Console.WriteLine("добавление");
                        lootboxPoints.Add(increment.MatchRewardTypeId.Value, increment.LootboxPoints);
                    }
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