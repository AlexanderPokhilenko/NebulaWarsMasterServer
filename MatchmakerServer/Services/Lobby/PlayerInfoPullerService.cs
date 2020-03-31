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
    /// Отвечает за получение данных про конкретный аккаунт из БД
    /// </summary>
    public class PlayerInfoPullerService
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerInfoPullerService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        [ItemCanBeNull]
        public async Task<AccountInfo> GetPlayerInfo(string playerId)
        {
            Account account = await  dbContext.Accounts
                .Include(acc => acc.Warships)
                    .ThenInclude(warship => warship.WarshipType)
                .SingleOrDefaultAsync(account1 => account1.ServiceId==playerId);

            if (account == null)
            {
                return null;
            }

            return GetAccountInfo(account);
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