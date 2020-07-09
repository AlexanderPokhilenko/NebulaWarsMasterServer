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
        private readonly DbWarshipsStatisticsReader dbWarshipsStatisticsReader;
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;

        public QueueExtenderService(BattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            DbWarshipsStatisticsReader dbWarshipsStatisticsReader)
        {
        this.dbWarshipsStatisticsReader = dbWarshipsStatisticsReader;
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
        }
        
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        public async Task<bool> TryEnqueuePlayerAsync(string playerServiceId, int warshipId)
        {
            AccountDbDto accountDbDto = await dbWarshipsStatisticsReader.ReadAsync(playerServiceId);
            WarshipDbDto warship = accountDbDto.Warships.SingleOrDefault(dto => dto.Id == warshipId);

            if (warship == null)
            {
                Console.WriteLine("Корабль не принадлежит этому игроку");
                return false;
            }

            MatchEntryRequest matchEntryRequest = new MatchEntryRequest(playerServiceId, accountDbDto.Id, 
                warship.WarshipType.Name, warship.WarshipPowerLevel, warshipId, DateTime.UtcNow, 
                accountDbDto.Username);
            return battleRoyaleQueueSingletonServiceService.TryEnqueue(matchEntryRequest);
        }
    }
    
    
}