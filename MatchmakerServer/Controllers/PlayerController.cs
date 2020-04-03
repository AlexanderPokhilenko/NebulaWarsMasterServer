using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.ForControllers;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Принимает заявки на вход/выход из очереди в бой.
    /// Принимает сообщения о преждевременоом выходе из боя.
    /// Принимает запросы на результат конкретного боя.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
      
        private readonly PlayersAchievementsService achievementsService;
        private readonly BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService;
        private readonly BattleRoyaleQueueSingletonService queueSingletonService;
        private readonly MatchmakerFacadeService matchmakerFacadeService;

        public PlayerController(PlayersAchievementsService achievementsService,
            BattleRoyaleUnfinishedMatchesSingletonService unfinishedMatchesService, 
            BattleRoyaleQueueSingletonService queueSingletonService,
            MatchmakerFacadeService matchmakerFacadeService)
        {
            this.achievementsService = achievementsService;
            this.unfinishedMatchesService = unfinishedMatchesService;
            this.queueSingletonService = queueSingletonService;
            this.matchmakerFacadeService = matchmakerFacadeService;
        }

        /// <summary>
        /// Покидание боя. Нужно если, игрок вышел до окончания боя и хочет перезайти в другой бой.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Route(nameof(ExitFromBattle))]
        [HttpPost]
        public ActionResult ExitFromBattle([FromForm]string playerId)
        {
            Console.WriteLine(nameof(ExitFromBattle)+" был вызван");
            Console.WriteLine(nameof(playerId)+" "+playerId);
            
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();
            
            if (unfinishedMatchesService.TryRemovePlayerFromMatch(playerId))
            {
                return Ok();
            }
            else
            {
                Console.WriteLine("Не удалось удалить игрока");
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Отмена поиска боя. Нужно если игрок не хочет выходить в бой.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Route(nameof(DeleteFromQueue))]
        [HttpPost]
        public ActionResult<string> DeleteFromQueue([FromForm]string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
                return BadRequest();

            if (queueSingletonService.TryRemovePlayerFromQueue(playerId))
            {
                return Ok();
            }
            else
            {
                return StatusCode(409);
            }
        }

       
        /// <summary>
        /// Добавление в очередь. 
        /// </summary>
        [Route(nameof(GetRoomData))]
        [HttpPost]
        public async Task<ActionResult<string>> GetRoomData([FromForm]string playerId, [FromForm] int warshipId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }
            GameMatcherResponse matcherResponse = await matchmakerFacadeService.GetMatchData(playerId, warshipId);
            return DichSerialize(matcherResponse);
        }

        /// <summary>
        /// Получение результат матча для показа статистики после боя.
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="playerServiceId"></param>
        /// <returns></returns>
        [Route(nameof(GetMatchResult))]
        [HttpPost]
        public async Task<ActionResult<string>> GetMatchResult([FromForm] int? matchId, [FromForm] string playerServiceId)
        {
            Console.WriteLine(
                $"{nameof(GetMatchResult)} {nameof(matchId)} {matchId} {nameof(playerServiceId)} {playerServiceId}");
            //Чек на адекватность данных
            if (matchId == null || playerServiceId == null)
            {
                return BadRequest();
            }
            
            //Запрос в сервис
            var matchResult = await achievementsService.GetMatchResult(matchId.Value, playerServiceId);

            // matchResult = new PlayerAchievements()
            // {
            //     DoubleTokens = true,
            //     BattleRatingDelta = 5,
            //     OldSpaceshipRating = 8,
            //     RankingRewardTokens = 20,
            //     SpaceshipPrefabName = "Bird"
            // };
                
            //Чек на адекватность ответа
            if (matchResult == null)
            {
                Console.WriteLine("matchResult is null");
                return StatusCode(500);
            }
            return DichSerialize(matchResult);
        }
        
        private string DichSerialize<T>(T response)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(response);
            string stub = Convert.ToBase64String(data);
            return stub;  
        }
    }
}