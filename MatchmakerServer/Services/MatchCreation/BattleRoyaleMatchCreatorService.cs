using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.GameServerNegotiation;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    /// <summary>
    /// Пытается начать матч для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly MatchDbWriterService matchDbWriterService;
        private readonly MatchRoutingDataService matchRoutingDataService;
        private readonly IBattleRoyaleQueueSingletonService battleRoyaleQueue;
        private readonly IGameServerNegotiatorService gameServerNegotiatorService;
        private readonly BattleRoyaleBotFactoryService battleRoyaleBotFactoryService;
        private readonly IBattleRoyaleQueueSingletonService battleRoyaleQueueSingletonService;
        private readonly IBattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;

        public BattleRoyaleMatchCreatorService(
            MatchDbWriterService matchDbWriterService,
            MatchRoutingDataService matchRoutingDataService, 
            IBattleRoyaleQueueSingletonService battleRoyaleQueue,
            IGameServerNegotiatorService gameServerNegotiatorService,
            BattleRoyaleBotFactoryService battleRoyaleBotFactoryService,
            IBattleRoyaleQueueSingletonService battleRoyaleQueueSingletonService,
            IBattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService)
        {
            this.battleRoyaleQueue = battleRoyaleQueue;
            this.matchDbWriterService = matchDbWriterService;
            this.matchRoutingDataService = matchRoutingDataService;
            this.unfinishedMatchesService = unfinishedMatchesService;
            this.gameServerNegotiatorService = gameServerNegotiatorService;
            this.battleRoyaleBotFactoryService = battleRoyaleBotFactoryService;
            this.battleRoyaleQueueSingletonService = battleRoyaleQueueSingletonService;
        }
        
        public async Task<bool> TryCreateMatch(int numberOfPlayersInMatch, bool botsCanBeUsed)
        {
            GameUnits gameUnits = new GameUnits();

            List<MatchEntryRequest> requests = battleRoyaleQueueSingletonService
                .TakeMatchEntryRequests(numberOfPlayersInMatch);
            //Достать игроков из очереди без извлечения
            gameUnits.Players =requests
                .Select(request=>request.GetPlayerModel())
                .ToList();
            
            //Если нужно дополнить ботами
            if (0 < gameUnits.Players.Count && gameUnits.Players.Count < numberOfPlayersInMatch)
            {
                //и можно дополнить ботами
                if (botsCanBeUsed)
                {
                    //дополнить
                    int numberOfBots = numberOfPlayersInMatch - gameUnits.Players.Count;
                    gameUnits.Bots = battleRoyaleBotFactoryService.CreateBotModels(numberOfBots);
                }
            }
            
            //Достаточно игроков?
            if (gameUnits.Players.Count + gameUnits.Bots?.Count != numberOfPlayersInMatch)
            {
                return false;
            }

            // Присвоить временные id игрокам на один бой 
             List<ushort> playerTmpIds = PlayerTemporaryIdsFactory.Create(gameUnits.Players.Count);
             for(int i = 0; i < gameUnits.Players.Count; i++)
             {
                 PlayerModel playerModel = gameUnits.Players[i];
                 playerModel.TemporaryId = playerTmpIds[i];
             }

            //На каком сервере будет запускаться матч?
            MatchRoutingData matchRoutingData = matchRoutingDataService.GetMatchRoutingData();

            //Сделать запись об матче в БД
            Match match = await matchDbWriterService
                .WriteAsync(matchRoutingData, requests.Select(request => request.GetWarshipId()).ToList());

            //Создать объект с информацией про бой
            BattleRoyaleMatchModel matchModel = BattleRoyaleMatchDataFactory.Create(gameUnits, match);
            
            //Добавить игроков в таблицу тех кто в бою
            unfinishedMatchesService.AddPlayersToMatch(matchModel);
            
            //Извлечь игроков из очереди
            battleRoyaleQueue.RemovePlayersFromQueue(matchModel.GameUnits.Players);
            
            //Сообщить на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(matchModel);

            return true;
        }
    }
}

