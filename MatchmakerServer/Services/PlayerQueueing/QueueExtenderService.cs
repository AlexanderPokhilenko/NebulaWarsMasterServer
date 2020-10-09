using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class QueueExtenderService : IQueueExtenderService
    {
        private readonly IDbAccountWarshipReaderService dbAccountWarshipReaderService;
        private readonly IBattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService;

        public QueueExtenderService(IBattleRoyaleQueueSingletonService battleRoyaleQueueSingletonServiceService,
            IDbAccountWarshipReaderService dbAccountWarshipReaderService)
        {
            this.battleRoyaleQueueSingletonServiceService = battleRoyaleQueueSingletonServiceService;
            this.dbAccountWarshipReaderService = dbAccountWarshipReaderService;
        }

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