using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<AccountInfo> GetAccountInfo([NotNull] string serviceId)
        {
            Account account = await  dbContext.Accounts
                .Include(account1 => account1.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId == serviceId);

            if (account == null)
            {
                return null;
            }
            
            foreach (var warship in account.Warships)
            {
                warship.Rating = warship.MatchResultForPlayers.Sum(result => result.WarshipRatingDelta) ?? 0;
                Console.WriteLine($"{nameof(warship.Rating)} {warship.Rating}");
            }

            account.Rating = account.Warships.Sum(warship => warship.Rating);
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