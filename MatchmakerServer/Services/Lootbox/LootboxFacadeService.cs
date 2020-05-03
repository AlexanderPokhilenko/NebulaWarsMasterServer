using System.Threading.Tasks;
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

        public LootboxFacadeService(SmallLootboxOpenAllowingService allowingService,
            SmallLootboxDataFactory smallLootboxDataFactory,
            LootboxDbWriterService lootboxDbWriterService)
        {
            this.allowingService = allowingService;
            this.smallLootboxDataFactory = smallLootboxDataFactory;
            this.lootboxDbWriterService = lootboxDbWriterService;
        }

        
        public async Task<LootboxData> TryGetLootboxData(string playerServiceId)
        {
            //Игрок может открыть лутбокс?
            if (!await allowingService.CanPlayerOpenLootbox(playerServiceId))
            {
                return null;
            }

            //Создать лутбокс
            LootboxData lootboxData = smallLootboxDataFactory.Create();
            //Сохранить лутбокс 
            await lootboxDbWriterService.Write(playerServiceId, lootboxData);
            return lootboxData;
        }
    }
}