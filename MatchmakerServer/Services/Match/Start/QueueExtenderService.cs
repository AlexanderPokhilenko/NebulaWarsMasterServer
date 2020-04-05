using System;
using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services
{
    //TODO говно
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь
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
            //В БД есть эти данные и они полные?
            var (success, warship) = await warshipValidatorService.GetWarshipById(playerServiceId, warshipId);
            if (!success)
            {
                return false;
            }
            
            
            QueueInfoForPlayer playerInfo = new QueueInfoForPlayer(warship.Account.ServiceId, warship.AccountId, 
                warship.WarshipType.Name, warship.CombatPowerLevel, warshipId, DateTime.UtcNow);
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerInfo);
        }
    }
}