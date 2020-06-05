using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.MatchFinishing;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
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
        private readonly PlayerMatchResultDbReaderService matchResultDbReaderService;
        private readonly BattleRoyaleQueueSingletonService queueSingletonService;
        private readonly MatchmakerFacadeService matchmakerFacadeService;

        public PlayerController(PlayerMatchResultDbReaderService matchResultDbReaderService,
            BattleRoyaleQueueSingletonService queueSingletonService,
            MatchmakerFacadeService matchmakerFacadeService)
        {
            this.matchResultDbReaderService = matchResultDbReaderService;
            this.queueSingletonService = queueSingletonService;
            this.matchmakerFacadeService = matchmakerFacadeService;
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
            {
                return BadRequest();
            }

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
        [Route(nameof(GetMatchData))]
        [HttpPost]
        public async Task<ActionResult<string>> GetMatchData([FromForm]string playerId, [FromForm] int warshipId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }
            MatchmakerResponse matcherResponse = await matchmakerFacadeService.GetMatchData(playerId, warshipId);
            return matcherResponse.SerializeToBase64String();
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
            if (matchId == null)
            {
                Console.WriteLine($"{nameof(matchId)} is null");
                return BadRequest();
            }

            if (playerServiceId == null)
            {
                Console.WriteLine($"{nameof(playerServiceId)} is null");
                return BadRequest();
            }
            
            MatchResult matchResult = await matchResultDbReaderService
                .GetMatchResult(matchId.Value, playerServiceId);
            
            //Чек на адекватность ответа
            if (matchResult == null)
            {
                Console.WriteLine("\n\n\n\n\nmatchResult is null");
                return StatusCode(500);
            }

            if (matchResult.CurrentSpaceshipRating < 0)
            {
                Console.WriteLine($"\n\n\n\n{nameof(matchResult.CurrentSpaceshipRating)} {matchResult.CurrentSpaceshipRating }");
                return StatusCode(500);
            }
            return matchResult.SerializeToBase64String();
        }
    }
}