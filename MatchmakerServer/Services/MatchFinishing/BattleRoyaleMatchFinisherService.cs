using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    /// <summary>
    /// Отвечает за дописывания результатов боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchFinisherService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService;
        private readonly BattleRoyaleMatchRewardService battleRoyaleMatchRewardService;

        public BattleRoyaleMatchFinisherService(ApplicationDbContext dbContext,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesSingletonService,
            BattleRoyaleMatchRewardService battleRoyaleMatchRewardService)
        {
            this.dbContext = dbContext;
            this.unfinishedMatchesSingletonService = unfinishedMatchesSingletonService;
            this.battleRoyaleMatchRewardService = battleRoyaleMatchRewardService;
        }
        
        public async Task PlayerDeath(int accountId, int placeInMatch, int matchId)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"{nameof(accountId)} {accountId}");
            Console.WriteLine($"{nameof(placeInMatch)} {placeInMatch}");
            Console.WriteLine($"{nameof(matchId)} {matchId}");
            Console.WriteLine();
            Console.WriteLine();
            
            MatchResultForPlayer matchResultForPlayer = await dbContext
                .MatchResultForPlayers
                .SingleOrDefaultAsync(matchResult => 
                    matchResult.MatchId == matchId 
                    && matchResult.Warship.AccountId == accountId);

            
            if (matchResultForPlayer == null)
            {
                Console.WriteLine("\n matchResultForPlayer is null\n");
                return;
            }

            Account account = await dbContext.Accounts.SingleAsync(account1 => account1.Id == accountId);
            Warship warship = await dbContext.Warships
                .SingleAsync(warship1 => warship1.Id == matchResultForPlayer.WarshipId);
            
            int currentWarshipRating = warship.WarshipRating;
            MatchReward matchReward = battleRoyaleMatchRewardService.GetMatchReward(placeInMatch, currentWarshipRating);

            
            matchResultForPlayer.PlaceInMatch = placeInMatch;
            matchResultForPlayer.PremiumCurrencyDelta = matchReward.PremiumCurrencyDelta;
            matchResultForPlayer.RegularCurrencyDelta = matchReward.RegularCurrencyDelta;
            matchResultForPlayer.WarshipRatingDelta = matchReward.WarshipRatingDelta;
            matchResultForPlayer.PointsForBigLootbox = matchReward.PointsForBigChest;
            matchResultForPlayer.PointsForSmallLootbox = matchReward.PointsForSmallLootbox;

            LogMatchResult(matchResultForPlayer);
            
            //изменить денормализованные показатели рейтинга
            warship.WarshipRating += matchResultForPlayer.WarshipRatingDelta.Value;
            account.Rating += matchResultForPlayer.WarshipRatingDelta.Value;
            
            await dbContext.SaveChangesAsync();
            
            //удаление игрока из структуры данных
            bool success = unfinishedMatchesSingletonService.TryRemovePlayerFromMatch(account.ServiceId);
            if (!success)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Не удалось удалить игрока из матча ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void LogMatchResult(MatchResultForPlayer matchResultForPlayer)
        {
    
            Console.WriteLine($"{nameof(matchResultForPlayer.PremiumCurrencyDelta)} {matchResultForPlayer.PremiumCurrencyDelta}");
            Console.WriteLine($"{nameof(matchResultForPlayer.RegularCurrencyDelta)} {matchResultForPlayer.RegularCurrencyDelta}");
            Console.WriteLine($"{nameof(matchResultForPlayer.WarshipRatingDelta)} {matchResultForPlayer.WarshipRatingDelta}");
            Console.WriteLine($"{nameof(matchResultForPlayer.PointsForBigLootbox)} {matchResultForPlayer.PointsForBigLootbox}");
            Console.WriteLine($"{nameof(matchResultForPlayer.PointsForSmallLootbox)} {matchResultForPlayer.PointsForSmallLootbox}");
        }
        
        public async Task DeleteRoom(int matchId)
        {
            await AddResultsOfMatchToDatabase(matchId);
            bool success = unfinishedMatchesSingletonService.TryRemoveMatch(matchId);
            if (!success)
            {
                Console.WriteLine("\nНе удалось удалить матч\n");
            }
        }

        private async Task AddResultsOfMatchToDatabase(int matchId)
        {
            //Поставить дату окончания матча
            var match = await dbContext.Matches
                .Include(match1 => match1.MatchResultForPlayers)
                .Where(match1 => match1.Id == matchId)
                .SingleOrDefaultAsync();

            match.FinishTime = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            
            //Дозаписать результаты для победителей
            //Для них результаты не были записаны, так как они не умирали
            var incompleteMatchResults = match.MatchResultForPlayers
                .Where(matchResult => matchResult.RegularCurrencyDelta == null);

            int index = 0;
            foreach (var matchResultForPlayer in incompleteMatchResults)
            {
                Console.WriteLine($"\nДозапись результата матча для игрока {matchResultForPlayer.Warship.AccountId}\n");
                int placeInMatch = ++index;
                await PlayerDeath(matchResultForPlayer.Warship.AccountId, placeInMatch, matchId);
            }
        }
    }
}