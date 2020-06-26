using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.PlayerQueueing;
using AmoebaGameMatcherServer.Services.Queues;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

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
        private readonly BattleRoyaleQueueSingletonService queueSingletonService;
        private readonly MatchmakerFacadeService matchmakerFacadeService;

        public PlayerController(BattleRoyaleQueueSingletonService queueSingletonService,
            MatchmakerFacadeService matchmakerFacadeService)
        {
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
            MatchmakerResponse matcherResponse = await matchmakerFacadeService.GetMatchDataAsync(playerId, warshipId);
            return matcherResponse.SerializeToBase64String();
        }
    }
}