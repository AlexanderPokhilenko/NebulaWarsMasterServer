using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
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
        public async Task<AccountInfo> GetAccountInfo([NotNull] string playerId)
        {
            Account account = await  dbContext.Accounts
                .Include(acc => acc.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId==playerId);

            if (account == null)
            {
                return null;
            }

            Console.WriteLine($"{nameof(account.Rating)} {account.Rating}");
            return GetAccountInfo(account);
        }

        private AccountInfo GetAccountInfo(Account account)
        {
            AccountInfo accountInfo = new AccountInfo
            {
                Username = account.Username,
                PremiumCurrency = account.PremiumCurrency,
                RegularCurrency = account.RegularCurrency,
                PointsForBigChest = account.PointsForBigChest,
                PointsForSmallChest = account.PointsForSmallChest,
                AccountRating = account.Rating,
                Warships = new List<WarshipCopy>()
            };
            
            foreach (var warship in account.Warships)
            {
                WarshipCopy warshipCopy = new WarshipCopy
                {
                    Id = warship.Id,
                    PrefabName = warship.WarshipType.Name,
                    Rating = warship.Rating,
                    CombatPowerLevel = warship.CombatPowerLevel,
                    CombatPowerValue = warship.CombatPowerValue
                };
                accountInfo.Warships.Add(warshipCopy);
            }
            
            return accountInfo;
        }
    }
}