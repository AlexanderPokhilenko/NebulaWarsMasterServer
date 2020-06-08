using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.GameServerNegotiation;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    /// <summary>
    /// Полностью управляет созданием боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly BattleRoyaleMatchPackerService battleRoyaleMatchPackerService;
        private readonly IGameServerNegotiatorService gameServerNegotiatorService;
        private readonly MatchRoutingDataService matchRoutingDataService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;
        private readonly MatchDbWriterService matchDbWriterService;
        private readonly BattleRoyaleQueueSingletonService battleRoyaleQueue;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            IGameServerNegotiatorService gameServerNegotiatorService,
            MatchRoutingDataService matchRoutingDataService, 
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService,
            BattleRoyaleQueueSingletonService battleRoyaleQueue,
            MatchDbWriterService matchDbWriterService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            
            this.gameServerNegotiatorService = gameServerNegotiatorService;
            this.matchRoutingDataService = matchRoutingDataService;
            this.unfinishedMatchesService = unfinishedMatchesService;
            this.battleRoyaleQueue = battleRoyaleQueue;
            this.matchDbWriterService = matchDbWriterService;
        }
        
        public async Task<MatchCreationMessage> TryCreateMatch(int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            //Достать игроков из очереди без извлечения
            var (success, gameUnitsForMatch, playersQueueInfo) = battleRoyaleMatchPackerService
                .GetPlayersForMatch(maxNumberOfPlayersInBattle, botsCanBeUsed);

            //Достаточно игроков?
            if (!success)
            {
                return new MatchCreationMessage
                {
                    Success = false,
                    FailureReason = MatchCreationFailureReason.NotEnoughPlayers,
                    MatchId = null
                };
            }

            //На каком сервере будет запускаться матч?
            var matchRoutingData = matchRoutingDataService.GetMatchRoutingData();

            //Сделать запись об матче в БД
            Match match = await matchDbWriterService.WriteMatchDataToDb(matchRoutingData, playersQueueInfo);

            //Создать объект с информацией про бой
            BattleRoyaleMatchModel matchModel = BattleRoyaleMatchDataFactory.Create(gameUnitsForMatch, match);
            
            //Добавить игроков в таблицу тех кто в бою
            unfinishedMatchesService.AddPlayersToMatch(matchModel);
            
            //Извлечь игроков из очереди
            battleRoyaleQueue.RemovePlayersFromQueue(matchModel.GameUnitsForMatch.Players);
            
            //Сообщить на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(matchModel);
            
            return new MatchCreationMessage
            {
                Success = true,
                FailureReason = null,
                MatchId = matchModel.MatchId
            };
        }
    }
}

