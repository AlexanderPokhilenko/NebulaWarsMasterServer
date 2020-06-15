using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Queues;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    //TODO добавить в ответ сообщение об ошибке, если в БД не было найдено указанных игроком данных.
    /// <summary>
    /// Отвечает за обработку запросов на вход в бой от клиентов.
    /// </summary>
    public class MatchmakerFacadeService
    {
        private readonly QueueExtenderService queueExtenderService;
        private readonly BattleRoyaleQueueSingletonService queueSingletonService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;

        public MatchmakerFacadeService(QueueExtenderService queueExtenderService, 
            BattleRoyaleQueueSingletonService queueSingletonService,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService)
        {
            this.queueExtenderService = queueExtenderService;
            this.queueSingletonService = queueSingletonService;
            this.unfinishedMatchesService = unfinishedMatchesService;
        }

        [ItemNotNull]
        public async Task<MatchmakerResponse> GetMatchDataAsync([NotNull] string playerId, int warshipId)
        {
            //Данные для окна ожидания боя
            MatchmakerResponse response = new MatchmakerResponse
            {
                NumberOfPlayersInQueue = queueSingletonService.GetNumberOfPlayersInQueue(),
                NumberOfPlayersInBattles = unfinishedMatchesService.GetNumberOfPlayersInBattles()
            };
            
            //Игрок в очереди?
            if (queueSingletonService.IsPlayerInQueue(playerId))
            {
                Console.WriteLine("IsPlayerInQueue");
                response.PlayerInQueue = true;
                return response;
            }
            //Игрок в бою?
            else if (unfinishedMatchesService.IsPlayerInMatch(playerId))
            {
                Console.WriteLine("IsPlayerInMatch");
                BattleRoyaleMatchModel matchModel = unfinishedMatchesService.GetMatchModel(playerId);
                response.PlayerInBattle = true;
                response.BattleRoyaleMatchModel = matchModel;
                return response;
            }
            //Добавить в очередь
            else
            {
                Console.WriteLine("TryEnqueuePlayerAsync");
                bool successfulQueuing = await queueExtenderService.TryEnqueuePlayerAsync(playerId, warshipId); 
                if (!successfulQueuing)
                {
                    throw new Exception("Не удалось зарегистрировать игрока.");
                }
                response.PlayerHasJustBeenRegistered = true;
                return response;
            }
        }
    }
}