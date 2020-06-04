using System;
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
            // (
                //        select sum(mr1.""WarshipRatingDelta"")
                //            from ""MatchResultForPlayers"" mr1
                //            where mr1.""WarshipId""  = mr.""WarshipId"" 
                //    )
    string sql = @"select   a.""Id"", 
                            a.""ServiceId"",
                            a.""Username"", 
                            a.""RegularCurrency"", 
                            a.""PremiumCurrency"", 
                            a.""PointsForSmallLootbox"", 
                            a.""CreationDate"", 
                            sum(mr.""WarshipRatingDelta"") as Rating
                            

                 from ""Accounts"" a
                 inner join ""Warships"" w on a.""Id"" = w.""AccountId""  
                 inner join ""MatchResultForPlayers"" mr on w.""Id"" = mr.""WarshipId""  
                 inner join ""WarshipTypes"" wt on w.""WarshipTypeId"" = wt.""Id"" 
                 where a.""ServiceId"" = {0}
                 
                 GROUP BY a.""Id"" 
                 ";

            
            Account account = await dbContext.Accounts
                .FromSql(new RawSqlString(sql), serviceId)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                Console.WriteLine($"account == null");
                return null;
            }
            
            Console.WriteLine(account.Id);
            Console.WriteLine(account.Rating);
            Console.WriteLine(account.Username);
            Console.WriteLine(account.CreationDate);
            Console.WriteLine(account.PremiumCurrency);
            Console.WriteLine(account.RegularCurrency);
            Console.WriteLine(account.ServiceId);
            Console.WriteLine(account.PointsForSmallLootbox);

            Console.WriteLine("account.Warships.Count="+account.Warships.Count);

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