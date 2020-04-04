using System;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services
{
    public class BattleRoyaleMatchRewardService
    {
        readonly BattleRoyaleWarshipRatingCalculator warshipRatingCalculator;
        public BattleRoyaleMatchRewardService()
        {
            warshipRatingCalculator = new BattleRoyaleWarshipRatingCalculator();
        }
        public MatchReward GetMatchReward(int placeInMatch, int currentWarshipRating)
        {
            //TODO добавить поддержку double tokens
            //TODO добавить поддержку сундуков
            //TODO решить, чт делать с Json-ом
            
            MatchReward result = new MatchReward
            {
                WarshipRatingDelta = GetWarshipRatingDelta(placeInMatch, currentWarshipRating),
                PremiumCurrencyDelta = 0,
                RegularCurrencyDelta = GetRegularCurrencyDelta(placeInMatch, currentWarshipRating),
                JsonMatchResultDetails = null,
                PointsForBigChest = 0,
                PointsForSmallChest = 0
            };
            return result;
        }

        private int GetRegularCurrencyDelta(int placeInMatch, int currentWarshipRating)
        {
            if (placeInMatch < 5)
            {
                return 10;
            }
            else
            {
                return 20;
            }
        }
        
        private int GetWarshipRatingDelta(int placeInMatch, int currentWarshipRating)
        {
            int warshipRatingDelta = warshipRatingCalculator.GetWarshipRatingDelta(currentWarshipRating, placeInMatch);
            return warshipRatingDelta;
        }
    }
    
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
            MatchResultForPlayer matchResultForPlayer = await dbContext
                .MatchResultForPlayers
                .SingleOrDefaultAsync(matchResult => 
                    matchResult.MatchId == matchId 
                    && matchResult.AccountId == accountId);

            
            if (matchResultForPlayer == null)
            {
                Console.WriteLine("matchResultForPlayer is null");
                return;
            }

            Warship warship = await dbContext.Warships
                .SingleAsync(warship1 => warship1.Id == matchResultForPlayer.WarshipId);
            
            int currentWarshipRating = warship.Rating;
            MatchReward matchReward = battleRoyaleMatchRewardService.GetMatchReward(placeInMatch, currentWarshipRating);

            matchResultForPlayer.PlaceInMatch = placeInMatch;
            matchResultForPlayer.PremiumCurrencyDelta = matchReward.PremiumCurrencyDelta;
            matchResultForPlayer.RegularCurrencyDelta = matchReward.RegularCurrencyDelta;
            matchResultForPlayer.WarshipRatingDelta = matchReward.WarshipRatingDelta;
            matchResultForPlayer.PointsForBigChest = matchReward.PointsForBigChest;
            matchResultForPlayer.PointsForSmallChest = matchReward.PointsForSmallChest;

            await dbContext.SaveChangesAsync();
            
            //удаление игрока из структуры данных
            //TODO это пиздец
            var account = await dbContext.Accounts.SingleAsync(account1 => account1.Id == accountId);

            bool success = unfinishedMatchesSingletonService.TryRemovePlayerFromMatch(account.ServiceId);
            if (!success)
            {
                Console.WriteLine("Произошла дичь. ");
            }
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
            Console.WriteLine($"\n{nameof(AddResultsOfMatchToDatabase)}\n");
            
            //Игроки для которых результаты боя не были записаны
            var matchResultForPlayers = dbContext.MatchResultForPlayers
                .Where(matchResultForPlayer => 
                        matchResultForPlayer.MatchId == matchId
                    && matchResultForPlayer.RegularCurrencyDelta==null)
                .ToList()
                ;
            
            for (int i = 0; i < matchResultForPlayers.Count(); i++)
            {
                var matchResultForPlayer = matchResultForPlayers[i];
                Console.WriteLine($"\nЗапись результата матча для игрока {matchResultForPlayer.AccountId}\n");
                int placeInMatch = i + 1;
                
                matchResultForPlayer.PlaceInMatch = placeInMatch;
                matchResultForPlayer.PremiumCurrencyDelta = 0;
                matchResultForPlayer.RegularCurrencyDelta = 10;
                matchResultForPlayer.WarshipRatingDelta = 5;
                matchResultForPlayer.PointsForBigChest = 0;
                matchResultForPlayer.PointsForSmallChest = 2;
            }
            
            await dbContext.SaveChangesAsync();
            Console.WriteLine($"\nМатч {nameof(matchId)} {matchId} был окончен\n");
        }
    }
}