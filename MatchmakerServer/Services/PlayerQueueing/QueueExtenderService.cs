using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь
    /// </summary>
    public class QueueExtenderService
    {
        private readonly DbAccountWarshipReaderService dbAccountWarshipReaderService;
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            DbAccountWarshipReaderService dbAccountWarshipReaderService)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.dbAccountWarshipReaderService = dbAccountWarshipReaderService;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayerAsync(string playerServiceId, int warshipId)
        {
            AccountDbDto accountDbDto = await dbAccountWarshipReaderService.ReadAsync(playerServiceId);
            if (accountDbDto == null)
            {
                return false;
            }
            
            WarshipDbDto warship = accountDbDto.Warships.SingleOrDefault(dto => dto.Id == warshipId);
            if (warship == null)
            {
                Console.WriteLine("Корабль не принадлежит этому игроку");
                return false;
            }

            string warshipSkinName = warship.CurrentSkinType.Name;
            Console.WriteLine(warshipSkinName);
            MatchEntryRequest matchEntryRequest = new MatchEntryRequest(playerServiceId, accountDbDto.Id, 
                warship.WarshipType.Name, warship.WarshipPowerLevel, warshipId, DateTime.UtcNow, 
                accountDbDto.Username, warshipSkinName);
            return battleRoyaleQueueSingletonServiceService.TryEnqueue(matchEntryRequest);
        }
    }
    
    
}