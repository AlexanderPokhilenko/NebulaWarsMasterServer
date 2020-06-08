using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь
    /// </summary>
    public class QueueExtenderService
    {
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;
        private readonly ApplicationDbContext dbContext;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            ApplicationDbContext dbContext)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.dbContext = dbContext;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayer(string playerServiceId, int warshipId)
        {
            //Достать информацию про корабль из БД. Нужно для балансировки по силе
            //+ проверка того, что корабль принадлежит этому игроку
            
            Warship warship = await dbContext.Warships
                .Include(warship1 => warship1.WarshipType)
                .Include(warship1 =>warship1.Account)
                .SingleOrDefaultAsync(warship1 => 
                    warship1.Id == warshipId 
                    && warship1.Account.ServiceId==playerServiceId);

            int warshipPowerPoints = await dbContext.LootboxPrizeWarshipPowerPoints
                .Where(war => war.WarshipId == warshipId)
                .SumAsync(lootbox => lootbox.Quantity);
            
            QueueInfoForPlayer playerInfo = new QueueInfoForPlayer(playerServiceId, warship.AccountId, 
                warship.WarshipType.Name, warshipPowerPoints, warshipId, DateTime.UtcNow);
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerInfo);
        }
    }
    
    
}