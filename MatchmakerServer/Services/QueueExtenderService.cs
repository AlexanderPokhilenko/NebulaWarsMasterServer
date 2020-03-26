using System.Threading.Tasks;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь (пока один режим)
    /// </summary>
    public class QueueExtenderService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;
        private readonly ApplicationDbContext applicationDbContext;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            ApplicationDbContext applicationDbContext)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.applicationDbContext = applicationDbContext;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрок в очередь.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns>Вернёт false если в БД нет такиз данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayer(string playerServiceId, int warshipId)
        {
            //Данные есть в БД?
            Warship warship = await GetFromDb(playerServiceId, warshipId);
            if (warship?.Account == null)
            {
                return false;
            }
            
            //Попытка добавить в очередь
            WarshipCopy warshipCopy = new WarshipCopy
            {
                Id = warship.Id,
                Rating = warship.Rating,
                PrefabName = warship.WarshipType.Name,
                CombatPowerLevel = warship.CombatPowerLevel,
                CombatPowerValue = warship.CombatPowerValue
            };
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerServiceId, warshipCopy);
        }

        /// <summary>
        /// Достаёт из БД корабль и аккаунт.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns></returns>
        private async Task<Warship> GetFromDb(string playerServiceId, int warshipId)
        {
            var warship = await applicationDbContext.Warships
                .Include(warship1 => warship1.WarshipType)
                .Include(warship1 =>warship1.Account)
                .SingleOrDefaultAsync(warship1 => 
                    warship1.Id == warshipId 
                    && warship1.Account.ServiceId==playerServiceId);
            return warship;
        }
    }
}