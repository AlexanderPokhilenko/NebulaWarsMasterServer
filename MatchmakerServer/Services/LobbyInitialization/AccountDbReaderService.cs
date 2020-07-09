using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Во время загрузки данных в лобби достаёт аккаунт из БД.
    /// Если такого аккаунта нет, то вернёт null.
    /// </summary>
    public class AccountDbReaderService
    {
        private readonly AccountResourcesDbReader accountResourcesDbReader;
        private readonly DbAccountWarshipReaderService dbAccountWarshipReaderService;

        public AccountDbReaderService(DbAccountWarshipReaderService dbAccountWarshipReaderService,
            AccountResourcesDbReader accountResourcesDbReader)
        {
            this.dbAccountWarshipReaderService = dbAccountWarshipReaderService;
            this.accountResourcesDbReader = accountResourcesDbReader;
        }


        [ItemCanBeNull]
        public async Task<AccountDbDto> ReadAccountAsync([NotNull] string playerServiceId)
        {
            AccountDbDto accountDbDto = await dbAccountWarshipReaderService.ReadAsync(playerServiceId);
            if (accountDbDto == null)
            {
                return null;
            }
            
            AccountResources accountResources = await accountResourcesDbReader.ReadAsync(playerServiceId);
            accountDbDto.HardCurrency = accountResources.HardCurrency;
            accountDbDto.SoftCurrency = accountResources.SoftCurrency;
            accountDbDto.LootboxPoints = accountResources.LootboxPoints;
            return accountDbDto;
        }
    }
}