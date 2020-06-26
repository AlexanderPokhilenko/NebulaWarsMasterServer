using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    
    /// <summary>
    /// Отвечает за дописывание результатов матча для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchFinisherService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService;
        private readonly BattleRoyaleMatchRewardCalculatorService battleRoyaleMatchRewardCalculatorService;
        private readonly WarshipReaderService warshipReaderService;

        public BattleRoyaleMatchFinisherService(ApplicationDbContext dbContext,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService,
            BattleRoyaleMatchRewardCalculatorService battleRoyaleMatchRewardCalculatorService,
            WarshipReaderService warshipReaderService)
        {
            this.dbContext = dbContext;
            this.unfinishedMatchesSingletonService = unfinishedMatchesSingletonService;
            this.battleRoyaleMatchRewardCalculatorService = battleRoyaleMatchRewardCalculatorService;
            this.warshipReaderService = warshipReaderService;
        }
        
        
        public async Task<bool> UpdatePlayerMatchResultInDbAsync(int accountId, int placeInMatch, int matchId)
        {
            Account account = await dbContext.Accounts.FindAsync(accountId);
            bool isPlayerInMatch = unfinishedMatchesSingletonService.IsPlayerInMatch(account.ServiceId, matchId);
            if (!isPlayerInMatch)
            {
                Console.WriteLine("Этот игрок не в бою UpdatePlayerMatchResultInDbAsync");
                return false;
            }
            
            //Достать результат боя из БД
            MatchResult matchResult = await dbContext.MatchResults
                .Where(matchResult1 => matchResult1.MatchId == matchId && matchResult1.Warship.AccountId == accountId)
                .SingleAsync();
            
            //Прочитать текущий рейтинг корабля. Он нужен для вычисления награды за бой.
            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(matchResult.WarshipId);
            
            //Вычислить награду за бой
            MatchReward matchReward = battleRoyaleMatchRewardCalculatorService
                .Calculate(placeInMatch, currentWarshipRating);
            
            //Обновить место в бою
            matchResult.PlaceInMatch = placeInMatch;

            //ОБновить ресурсы
            var increments = new List<Increment>();
            var decrements = new List<Decrement>();

            if (matchReward.SoftCurrency > 0)
            {
                increments.Add( new Increment
                {
                    SoftCurrency = matchReward.SoftCurrency,
                    IncrementTypeId = IncrementTypeEnum.Currency,
                    MatchRewardTypeId = MatchRewardTypeEnum.RankingReward 
                });
            }

            if (matchReward.LootboxPoints > 0)
            {
                increments.Add(
                    new Increment
                    {
                        LootboxPoints = matchReward.LootboxPoints,
                        IncrementTypeId = IncrementTypeEnum.Lootbox
                    });
            }
            
            if (matchReward.WarshipRatingDelta > 0)
            {
                increments.Add(new Increment
                {
                    WarshipRating = matchReward.WarshipRatingDelta,
                    IncrementTypeId = IncrementTypeEnum.WarshipRating,
                    WarshipId = matchResult.WarshipId
                });
            }
            else if(matchReward.WarshipRatingDelta < 0)
            {
                decrements.Add(new Decrement
                {
                    DecrementTypeId = DecrementTypeEnum.WarshipRating,
                    WarshipId = matchResult.WarshipId,
                    WarshipRating = Math.Abs(matchReward.WarshipRatingDelta)
                });
            }
            
            Transaction transaction = new Transaction
            {
                WasShown = false,
                DateTime = DateTime.UtcNow,
                Resources = new List<Resource>
                {
                    new Resource
                    {
                        Increments = increments,
                        Decrements = decrements,
                        ResourceTypeId = ResourceTypeEnum.MatchReward
                    }
                },
                TransactionTypeId = TransactionTypeEnum.MatchReward,
                AccountId = accountId
            };

            matchResult.Transaction = transaction;
            
            //Пометить, что игрок окончил бой
            matchResult.IsFinished = true;
            
            //Сохранить результат боя в БД
            await dbContext.SaveChangesAsync();
            
            //Удалить игрока из памяти
            bool success = unfinishedMatchesSingletonService.TryRemovePlayerFromMatch(account.ServiceId);
            if (!success)
            {
                throw new Exception("Не удалось удалить игрока из матча ");
            }

            return true;
        }

        public async Task FinishMatchAndWriteToDbAsync(int matchId)
        {
            //Поставить дату окончания матча
            Match match = await dbContext.Matches
                .Include(match1 => match1.MatchResults)
                .ThenInclude(matchResultResultForPlayer => matchResultResultForPlayer.Warship)
                .Where(match1 => match1.Id == matchId)
                .SingleOrDefaultAsync();
            match.FinishTime = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            
            //Дозаписать результаты для победителей
            //Для них результаты не были записаны, так как они не умирали
            var incompleteMatchResults = match.MatchResults
                .Where(matchResult => matchResult.IsFinished == false)
                .ToList();
            
            for(int i = 0; i < incompleteMatchResults.Count; i++)
            {
                MatchResult matchResult = incompleteMatchResults[i];
                int placeInMatch = ++i;
                await UpdatePlayerMatchResultInDbAsync(matchResult.Warship.AccountId, placeInMatch, matchId);
            }
            
            //Удалить матч из памяти
            bool success = unfinishedMatchesSingletonService.TryRemoveMatch(matchId);
            if (!success)
            {
                throw new Exception("Не удалось удалить матч");
            }
        }
    }
}