using System.Collections.Generic;
using System.Collections.Immutable;
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
        public async Task<AccountModel> GetAccountModel([NotNull] string serviceId)
        {
            Account account = await dbContext.Accounts
                .Include(account1 => account1.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId == serviceId);

            if (account == null)
            {
                return null;
            }

            //Заполнить рейтинг и значение очков силы для кораблей
            foreach (var warship in account.Warships)
            {
                warship.Rating = await dbContext.MatchResultForPlayers
                     .Where(matchResultForPlayer =>
                         matchResultForPlayer.WarshipId == warship.Id && matchResultForPlayer.WarshipRatingDelta != null)
                     .SumAsync(result => result.WarshipRatingDelta) ?? 0;
                warship.PowerPoints = await dbContext.LootboxPrizeWarshipPowerPoints
                    .Where(powerPoints => powerPoints.WarshipId == warship.Id)
                    .SumAsync(powerPoints => powerPoints.Quantity);
            }

            //Заполнить рейтинг аккаунта
            account.Rating = account.Warships.Sum(warship => warship.Rating);

            //Заполнить значение валюты из побед в бою
            account.RegularCurrency = await dbContext.MatchResultForPlayers
                .Where(matchResultForPlayer =>matchResultForPlayer.Warship.AccountId == account.Id)
                .SumAsync(matchResultForPlayer => matchResultForPlayer.RegularCurrencyDelta) ?? 0;

            //Заполнить значение валюты из лутбоксов
            account.RegularCurrency += await dbContext.LootboxPrizeRegularCurrencies
                .Where(prize => prize.LootboxDb.AccountId == account.Id)
                .SumAsync(prize => prize.Quantity);

            //Отнять значения покупок улучшений
            account.RegularCurrency -= await dbContext.WarshipImprovementPurchases
                .Where(purchase => purchase.Warship.AccountId == account.Id)
                .SumAsync(purchase => purchase.RegularCurrencyCost);
            
            return GetAccountModel(account);
        }

        //TODO тут должн быть адаптер
        private AccountModel GetAccountModel(Account account)
        {
            AccountModel accountInfo = new AccountModel
            {
                Username = account.Username,
                PremiumCurrency = account.PremiumCurrency,
                RegularCurrency = account.RegularCurrency,
                PointsForBigLootbox = account.PointsForBigLootbox,
                PointsForSmallLootbox = account.PointsForSmallLootbox,
                AccountRating = account.Rating,
                Warships = new List<WarshipModel>()
            };
            
            foreach (var warship in account.Warships)
            {
                WarshipModel warshipModel = new WarshipModel();
                warshipModel.Id = warship.Id;
                warshipModel.PrefabName = warship.WarshipType.Name;
                warshipModel.Rating = warship.Rating;
                warshipModel.PowerLevel = warship.PowerLevel;
                warshipModel.PowerPoints = warship.PowerPoints;
                warshipModel.Description = warship.WarshipType.Description;
                warshipModel.CombatRoleName = warship.WarshipType.WarshipCombatRole.Name;
                accountInfo.Warships.Add(warshipModel);
            }
            
            return accountInfo;
        }
    }
}