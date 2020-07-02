using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
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
        private readonly ApplicationDbContext dbContext;
        private readonly DbAccountWarshipsReader dbAccountWarshipsReader;
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            ApplicationDbContext dbContext, DbAccountWarshipsReader dbAccountWarshipsReader)
        {
            this.dbContext = dbContext;
            this.dbAccountWarshipsReader = dbAccountWarshipsReader;
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <param name="playerServiceId"></param>
        /// <param name="warshipId"></param>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayerAsync(string playerServiceId, int warshipId)
        {
            //Достать информацию про корабль из БД. Нужно для балансировки по силе
            //+ проверка того, что корабль принадлежит этому игроку

            var accountDbDto = await dbAccountWarshipsReader.GetAccountWithWarshipsAsync(playerServiceId);

            var warship = accountDbDto.Warships
                .SingleOrDefault(dto => dto.Id == warshipId);

            if (warship == null)
            {
                Console.WriteLine("Корабль не принадлежит этому игроку");
                return false;
            }

            QueueInfoForPlayer playerInfo = new QueueInfoForPlayer(playerServiceId, accountDbDto.Id, 
                warship.WarshipType.Name, warship.WarshipPowerLevel, warshipId, DateTime.UtcNow);
            return battleRoyaleQueueSingletonServiceService.TryEnqueuePlayer(playerInfo);
        }
    }
    
    
}