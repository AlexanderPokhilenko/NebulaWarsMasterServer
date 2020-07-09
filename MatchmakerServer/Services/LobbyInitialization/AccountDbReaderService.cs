using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;
using Npgsql;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД.
    /// Если такого аккаунта нет, то вернёт null.
    /// </summary>
    public class AccountDbReaderService
    {
        private readonly SkinsDbReaderService skinsDbReaderService;
        private readonly DbAccountWarshipsReader dbAccountWarshipsReader;
        private readonly AccountResourcesDbReader accountResourcesDbReader;

        public AccountDbReaderService(NpgsqlConnection connection, DbAccountWarshipsReader dbAccountWarshipsReader,
            SkinsDbReaderService skinsDbReaderService)
        {
            this.dbAccountWarshipsReader = dbAccountWarshipsReader;
            this.skinsDbReaderService = skinsDbReaderService;
            accountResourcesDbReader = new AccountResourcesDbReader(connection);
        }

        [ItemCanBeNull]
        public async Task<AccountDbDto> ReadAccountAsync([NotNull] string serviceId)
        {
            AccountDbDto account = await dbAccountWarshipsReader.GetAccountWithWarshipsAsync(serviceId);
            if (account == null)
            {
                return null;
            }

            //заполнить скины для всех кораблей
            Dictionary<int, List<string>> skinsDict = await skinsDbReaderService.ReadAsync(account.Id);
            if (skinsDict == null || skinsDict.Count == 0)
            {
                throw new Exception("warship has no skin");
            }
            
            foreach (var (warshipId, list) in skinsDict)
            {
                account.Warships
                    .Single(warship => warship.Id == warshipId)
                    .Skins.AddRange(list);
            }

            foreach (var warshipDbDto in account.Warships)
            {
                if (warshipDbDto.WarshipPowerLevel == 0)
                {
                    throw new Exception("Нулевой уровень "+nameof(AccountDbReaderService));
                }
                
                if (warshipDbDto.Skins == null || warshipDbDto.Skins.Count == 0)
                {
                    throw new Exception("Warship have no skins");
                }
            }
            
            AccountResources accountResources = await accountResourcesDbReader.GetAccountResourcesAsync(serviceId);
            account.HardCurrency = accountResources.HardCurrency;
            account.SoftCurrency = accountResources.SoftCurrency;
            account.LootboxPoints = accountResources.LootboxPoints;
            return account;
        }
    }
}