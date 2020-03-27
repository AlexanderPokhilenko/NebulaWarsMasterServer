using System.Threading.Tasks;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Полностью управляет созданием боя для батл рояль режима.
    /// </summary>
    public class BattleRoyaleMatchCreatorService
    {
        private readonly BattleRoyaleMatchPackerService battleRoyaleMatchPackerService;
        private readonly IGameServerNegotiatorService gameServerNegotiatorService;
        private readonly MatchmakerDichService matchmakerDichService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;
        private readonly MatchDataDbWriterService matchDataDbWriterService;
        private readonly QueueHelperSukaService sukaService;

        public BattleRoyaleMatchCreatorService(BattleRoyaleMatchPackerService battleRoyaleMatchPackerService, 
            IGameServerNegotiatorService gameServerNegotiatorService,
            MatchmakerDichService matchmakerDichService, 
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService, QueueHelperSukaService sukaService,
            MatchDataDbWriterService matchDataDbWriterService)
        {
            this.battleRoyaleMatchPackerService = battleRoyaleMatchPackerService;
            
            this.gameServerNegotiatorService = gameServerNegotiatorService;
            this.matchmakerDichService = matchmakerDichService;
            this.unfinishedMatchesService = unfinishedMatchesService;
            this.sukaService = sukaService;
            this.matchDataDbWriterService = matchDataDbWriterService;
        }
        
        public async Task<MatchCreationMessage> 
            TryCreateMatch(int maxNumberOfPlayersInBattle, bool botsCanBeUsed)
        {
            //Попробовать достать игроков из очереди
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

            //На каком сервере будет запучаться матч?
            var matchRoutingData = matchmakerDichService.GetMatchRoutingData();

            //Сделать запись об матче в БД
            Match match = await matchDataDbWriterService.WriteMatchDataToDb(matchRoutingData, playersQueueInfo);

            //Создать объект со всей инфой про бой
            BattleRoyaleMatchData matchData = MatchDataFactory.Create(gameUnitsForMatch, match);
            
            //Добавить игроков в таблицу тех кто в бою
            unfinishedMatchesService.AddPlayersToMatch(matchData);
            
            //Извлечь игроков из очереди
            sukaService.RemovePlayersFromQueue(matchData.Players);
            
            //Сообщить на гейм сервер
            await gameServerNegotiatorService.SendRoomDataToGameServerAsync(matchData);
            
            return new MatchCreationMessage
            {
                Success = true,
                FailureReason = null,
                MatchId = matchData.MatchId
            };
        }
    }

    public enum MatchCreationFailureReason
    {
        NotEnoughPlayers
    }
}

