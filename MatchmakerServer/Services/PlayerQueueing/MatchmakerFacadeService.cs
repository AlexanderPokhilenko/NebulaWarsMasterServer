using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Отвечает за обработку запросов на вход в бой от клиентов.
    /// </summary>
    public class MatchmakerFacadeService
    {
        private readonly QueueExtenderService queueExtenderService;
        private readonly BattleRoyaleQueueSingletonService queueSingletonService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;

        public MatchmakerFacadeService(
            QueueExtenderService queueExtenderService, 
            BattleRoyaleQueueSingletonService queueSingletonService,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService)
        {
            this.queueExtenderService = queueExtenderService;
            this.queueSingletonService = queueSingletonService;
            this.unfinishedMatchesService = unfinishedMatchesService;
        }

        [ItemNotNull]
        public async Task<MatchmakerResponse> GetMatchDataAsync([NotNull] string playerServiceId, int warshipId)
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