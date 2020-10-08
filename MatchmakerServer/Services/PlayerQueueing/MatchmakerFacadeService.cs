using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    public class MatchmakerFacadeService : IMatchmakerFacadeService
    {
        private readonly IQueueExtenderService queueExtenderService;
        private readonly IBattleRoyaleQueueSingletonService queueSingletonService;
        private readonly IBattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;

        public MatchmakerFacadeService(
            IQueueExtenderService queueExtenderService, 
            IBattleRoyaleQueueSingletonService queueSingletonService,
            IBattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService)
        {
            this.queueExtenderService = queueExtenderService;
            this.queueSingletonService = queueSingletonService;
            this.unfinishedMatchesService = unfinishedMatchesService;
        }

        public async Task<MatchmakerResponse> GetMatchDataAsync(string playerServiceId, int warshipId)
        {
            //Данные для окна ожидания боя
            MatchmakerResponse response = new MatchmakerResponse
            {
                NumberOfPlayersInQueue = queueSingletonService.GetNumberOfPlayers(),
                NumberOfPlayersInBattles = unfinishedMatchesService.GetNumberOfPlayersInBattles()
            };
            //Игрок в очереди?
            if (queueSingletonService.Contains(playerServiceId))
            {
                Console.WriteLine("PlayerInQueue");
                response.PlayerInQueue = true;
                return response;
            }
            //Игрок в бою?
            else if (unfinishedMatchesService.IsPlayerInMatch(playerServiceId))
            {
                Console.WriteLine("IsPlayerInMatch");
                BattleRoyaleMatchModel matchModel = unfinishedMatchesService.GetMatchModel(playerServiceId);
                response.PlayerInBattle = true;
                response.MatchModel = new BattleRoyaleClientMatchModel(matchModel, playerServiceId);
                return response;
            }
            //Добавить в очередь
            else
            {
                Console.WriteLine("TryEnqueuePlayerAsync");
                bool success = await queueExtenderService.TryEnqueuePlayerAsync(playerServiceId, warshipId); 
                if (!success)
                {
                    throw new Exception("Не удалось добавить игрока в очередь.");
                }
                response.PlayerHasJustBeenRegistered = true;
                return response;
            }
        }
    }
}