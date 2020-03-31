using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за получение данных про конкретный аккаунт из БД
    /// </summary>
    public class PlayerLobbyInitializeService
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerLobbyInitializeService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AccountInfo> GetPlayerInfo(string playerId)
        {
            Account account = await  dbContext.Accounts
                .Include(acc => acc.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId==playerId);

            if (account == null)
            {
                account = await CreateInDbDefaultAccount(playerId);
            }

            return GetAccountInfo(account);
        }

        private async Task<Account> CreateInDbDefaultAccount(string playerId)
        {
            Account account = DefaultAccountFactory.GetDefaultAccount(playerId);
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            return account;
        }
        
        private AccountInfo GetAccountInfo(Account account)
        {
            int accountRating = account.Warships.Sum(warship => warship.Rating);
            AccountInfo accountInfo = new AccountInfo
            {
                Username = account.Username,
                PremiumCurrency = account.PremiumCurrency,
                RegularCurrency = account.RegularCurrency,
                PointsForBigChest = account.PointsForBigChest,
                PointsForSmallChest = account.PointsForSmallChest,
                AccountRating = accountRating,
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

    public static class DefaultAccountFactory
    {
        private static readonly int defaultPremiumCurrency = 0;
        private static readonly int defaultRegularCurrency = 150;
        private static readonly int defaultPointsForBigChest = 100;
        private static readonly int DefaultPointsForSmallChest = 100;
        private static readonly int defaultWarshipCombatPowerLevel = 0;
        private static readonly int defaultWarshipCombatPowerValue = 0;
        private static readonly int warshipTypeHare = 1;
        private static readonly int warshipTypeBird = 2;
        private static int defaultWarshipRating = 0;

        public static Account GetDefaultAccount(string playerId)
        {
            Account account = new Account
            {
                ServiceId = playerId,
                Username = playerId,
                PremiumCurrency = defaultPremiumCurrency,
                RegistrationDate = DateTime.UtcNow,
                RegularCurrency = defaultRegularCurrency,
                PointsForBigChest = defaultPointsForBigChest,
                PointsForSmallChest = DefaultPointsForSmallChest,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        Rating = defaultWarshipRating,
                        WarshipTypeId = warshipTypeHare,
                        CombatPowerLevel = defaultWarshipCombatPowerLevel,
                        CombatPowerValue = defaultWarshipCombatPowerValue
                    },
                    new Warship
                    {
                        Rating = defaultWarshipRating,
                        WarshipTypeId = warshipTypeBird,
                        CombatPowerLevel = defaultWarshipCombatPowerLevel,
                        CombatPowerValue = defaultWarshipCombatPowerValue
                    }
                }
            };
            return account;
        }
    }
}