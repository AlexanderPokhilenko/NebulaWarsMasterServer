using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Управляет открытием лутбокса.
    /// </summary>
    public class LootboxFacadeService
    {
        private readonly SmallLootboxOpenAllowingService allowingService;
        private readonly SmallLootboxDataFactory smallLootboxDataFactory;
        private readonly LootboxDbWriterService lootboxDbWriterService;
        private readonly ApplicationDbContext dbContext;

        public LootboxFacadeService(SmallLootboxOpenAllowingService allowingService,
            SmallLootboxDataFactory smallLootboxDataFactory,
            LootboxDbWriterService lootboxDbWriterService,
            ApplicationDbContext dbContext)
        {
            this.allowingService = allowingService;
            this.smallLootboxDataFactory = smallLootboxDataFactory;
            this.lootboxDbWriterService = lootboxDbWriterService;
            this.dbContext = dbContext;
        }
        
        public async Task<LootboxModel> TryGetLootboxData(string playerServiceId)
        {
            //Игрок может открыть лутбокс?
            if (!await allowingService.CanPlayerOpenLootbox(playerServiceId))
            {
                return null;
            }

            int[] warshipIds = dbContext.Warships
                .Where(warship => warship.Account.ServiceId == playerServiceId)
                .Select(warship => warship.Id)
                .ToArray();

            //Создать лутбокс
            LootboxModel lootboxModel = smallLootboxDataFactory.Create(warshipIds);
            //Сохранить лутбокс 
            await lootboxDbWriterService.Write(playerServiceId, lootboxModel);
            return lootboxModel;
        }
    }
}