using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
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

        public LootboxFacadeService(SmallLootboxOpenAllowingService allowingService,
            SmallLootboxDataFactory smallLootboxModelFactory,
            LootboxDbWriterService lootboxDbWriterService,
            ApplicationDbContext dbContext)
        {
            this.allowingService = allowingService;
            this.smallLootboxModelFactory = smallLootboxModelFactory;
            this.lootboxDbWriterService = lootboxDbWriterService;
            this.dbContext = dbContext;
        }
        
        [ItemCanBeNull]
        public async Task<LootboxModel> CreateLootboxModelAsync([NotNull] string playerServiceId)
        {
            //У игрока достаточно денег на лутбокс?
            if (!await allowingService.CanPlayerOpenLootboxAsync(playerServiceId))
            {
                return null;
            }

            int[] warshipIds = dbContext.Warships
                .Where(warship => warship.Account.ServiceId == playerServiceId)
                .Select(warship => warship.Id)
                .ToArray();

            //Создать лутбокс
            LootboxModel lootboxModel = smallLootboxModelFactory.Create(warshipIds);
            
            //Сохранить лутбокс 
            await lootboxDbWriterService.WriteAsync(playerServiceId, lootboxModel);
            
            return lootboxModel;
        }
    }
}