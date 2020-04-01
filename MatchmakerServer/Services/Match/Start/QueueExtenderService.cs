using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    //TODO убрать отсюда BattleRoyaleQueueSingletonService
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь (пока один режим)
    /// </summary>
    public class QueueExtenderService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;
        private readonly IWarshipValidatorService warshipValidatorService;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            IWarshipValidatorService warshipValidatorService)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.warshipValidatorService = warshipValidatorService;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayer(string playerServiceId, int warshipId)
        {
            //В БД есть эти данные?
            var (success, warship) = await warshipValidatorService.GetWarshipById(playerServiceId, warshipId);
            if (!success)
            {
                return false;
            }
            
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerServiceId, warship);
        }
    }
}