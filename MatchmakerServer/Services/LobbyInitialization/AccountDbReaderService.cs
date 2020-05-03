using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД, если он есть.
    /// </summary>
    public class AccountDbReaderService
    {
        private readonly ApplicationDbContext dbContext;

        public AccountDbReaderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        /// <summary>
        /// Отвечает за получение данных про аккаунт из БД.
        /// </summary>
        [ItemCanBeNull]
        public async Task<AccountModel> GetAccountInfo([NotNull] string serviceId)
        {
            Account account = await dbContext.Accounts
                .Include(account1 => account1.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId == serviceId);

            if (account == null)
            {
                return null;
            }

            foreach (var warship in account.Warships)
            {
                var warship1 = warship;
                warship.Rating = await dbContext.MatchResultForPlayers
                    .Where(result =>
                     result.WarshipId == warship1.Id && result.WarshipRatingDelta != null)
                    .SumAsync(result => result.WarshipRatingDelta) ?? 0;
            }

            account.Rating = account.Warships.Sum(warship => warship.Rating);

            account.RegularCurrency = await dbContext.MatchResultForPlayers
                .Where(matchResultForPlayer =>matchResultForPlayer.Warship.AccountId == account.Id)
                .SumAsync(matchResultForPlayer => matchResultForPlayer.RegularCurrencyDelta) ?? 0;

            account.RegularCurrency += await dbContext.LootboxPrizeRegularCurrencies
                .Where(prize => prize.LootboxDb.AccountId == account.Id)
                .SumAsync(prize => prize.Quantity);

            account.PremiumCurrency = await dbContext.MatchResultForPlayers
                .Where(matchResultForPlayer =>
                    matchResultForPlayer.Warship.AccountId == account.Id)
                .SumAsync(matchResultForPlayer =>
                    matchResultForPlayer.PremiumCurrencyDelta) ?? 0;
            
            return GetAccountInfo(account);
        }

        //TODO тут должн быть адаптер
        private AccountModel GetAccountInfo(Account account)
        {
            AccountModel accountInfo = new AccountModel
            {
                Username = account.Username,
                PremiumCurrency = account.PremiumCurrency,
                RegularCurrency = account.RegularCurrency,
                PointsForBigLootbox = account.PointsForBigLootbox,
                PointsForSmallLootbox = account.PointsForSmallLootbox,
                AccountRating = account.Rating,
                Warships = new List<WarshipCopy>()
            };
            
            foreach (var warship in account.Warships)
            {
                WarshipCopy warshipCopy = new WarshipCopy();
                warshipCopy.Id = warship.Id;
                warshipCopy.PrefabName = warship.WarshipType.Name;
                warshipCopy.Rating = warship.Rating;
                warshipCopy.CombatPowerLevel = warship.CombatPowerLevel;
                warshipCopy.CombatPowerValue = warship.CombatPowerValue;
                accountInfo.Warships.Add(warshipCopy);
            }
            
            return accountInfo;
        }
    }
}