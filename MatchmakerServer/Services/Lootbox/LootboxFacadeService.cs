using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Lootbox
{
    /// <summary>
    /// Управляет открытием лутбокса.
    /// </summary>
    public class LootboxFacadeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly LootboxDbWriterService lootboxDbWriterService;
        private readonly SmallLootboxOpenAllowingService allowingService;
        private readonly SmallLootboxDataFactory smallLootboxModelFactory;
        private readonly AccountDbReaderService accountDbReaderService;


        public LootboxFacadeService(SmallLootboxOpenAllowingService allowingService,
            SmallLootboxDataFactory smallLootboxModelFactory,
            LootboxDbWriterService lootboxDbWriterService,
            ApplicationDbContext dbContext, AccountDbReaderService accountDbReaderService)
        {
            this.allowingService = allowingService;
            this.smallLootboxModelFactory = smallLootboxModelFactory;
            this.lootboxDbWriterService = lootboxDbWriterService;
            this.dbContext = dbContext;
            this.accountDbReaderService = accountDbReaderService;
        }
        
        [ItemCanBeNull]
        public async Task<LootboxModel> CreateLootboxModelAsync([NotNull] string playerServiceId)
        {
            //У игрока достаточно денег на лутбокс?
            if (!await allowingService.CanPlayerOpenLootboxAsync(playerServiceId))
            {
                return null;
            }

            AccountDbDto accountDbDto = await accountDbReaderService.ReadAccountAsync(playerServiceId);
            

            //Создать лутбокс
            LootboxModel lootboxModel = smallLootboxModelFactory.Create(accountDbDto.Warships);
            
            //Сохранить лутбокс 
            await lootboxDbWriterService.WriteAsync(playerServiceId, lootboxModel);
            
            return lootboxModel;
        }
    }
}