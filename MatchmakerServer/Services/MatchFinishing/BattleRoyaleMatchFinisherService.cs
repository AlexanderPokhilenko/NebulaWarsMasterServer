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
            //В памяти есть этот игрок?
            Account account = await dbContext.Accounts.FindAsync(accountId);
            bool isPlayerInMatch = unfinishedMatchesSingletonService.IsPlayerInMatch(account.ServiceId, matchId);
            if (!isPlayerInMatch)
            {
                Console.WriteLine("Этот игрок не в бою UpdatePlayerMatchResultInDbAsync");
                return false;
            }
            
            //Достать результат боя из БД
            MatchResultForPlayer matchResultForPlayer = await dbContext.MatchResultForPlayers
                .Where(matchResult => matchResult.MatchId == matchId && matchResult.Warship.AccountId == accountId)
                .SingleAsync();

            //Прочитать текущий рейтинг корабля. Он нужен для вычисления награды за бой.
            int currentWarshipRating = await warshipReaderService.ReadWarshipRatingAsync(matchResultForPlayer.WarshipId);
            
            //Вычислить награду за бой
            MatchReward matchReward = battleRoyaleMatchRewardCalculatorService
                .Calculate(placeInMatch, currentWarshipRating);
           
            //Обновить поля результата в БД 
            matchResultForPlayer.PlaceInMatch = placeInMatch;
            matchResultForPlayer.SoftCurrencyDelta = matchReward.SoftCurrencyDelta;
            matchResultForPlayer.WarshipRatingDelta = matchReward.WarshipRatingDelta;
            matchResultForPlayer.BigLootboxPoints = matchReward.BigLootboxPoints;
            matchResultForPlayer.SmallLootboxPoints = matchReward.SmallLootboxPoints;
            //Пометить, что игрок вышел окончил бой
            matchResultForPlayer.IsFinished = true;
            
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
                .Include(match1 => match1.MatchResultForPlayers)
                .ThenInclude(matchResultResultForPlayer => matchResultResultForPlayer.Warship)
                .Where(match1 => match1.Id == matchId)
                .SingleOrDefaultAsync();
            match.FinishTime = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            
            //Дозаписать результаты для победителей
            //Для них результаты не были записаны, так как они не умирали
            var incompleteMatchResults = match.MatchResultForPlayers
                .Where(matchResult => matchResult.IsFinished == false)
                .ToList();
            
            for(int i = 0; i < incompleteMatchResults.Count; i++)
            {
                MatchResultForPlayer matchResultForPlayer = incompleteMatchResults[i];
                int placeInMatch = ++i;
                await UpdatePlayerMatchResultInDbAsync(matchResultForPlayer.Warship.AccountId, placeInMatch, matchId);
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