using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.MatchFinishing;
using Libraries.NetworkLibrary.Experimental;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Принимает запросы на результат конкретного боя.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MatchResultController : ControllerBase
    {
        private readonly PlayerMatchResultDbReaderService matchResultDbReaderService;

        public MatchResultController(PlayerMatchResultDbReaderService matchResultDbReaderService)
        {
            this.matchResultDbReaderService = matchResultDbReaderService;
        }
        
        /// <summary>
        /// Получение результата матча для показа статистики после боя.
        /// </summary>
        /// <param name="matchId"></param>
        /// <param name="playerServiceId"></param>
        /// <returns></returns>
        [Route(nameof(Get))]
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] int? matchId, [FromQuery] string playerServiceId)
        {
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
            
            MatchResultDto matchResultDto = await matchResultDbReaderService
                .ReadMatchResultAsync(matchId.Value, playerServiceId);
            
            //Чек на адекватность ответа
            if (matchResultDto == null)
            {
                Console.WriteLine("\n\n\n\n\nmatchResult is null");
                return StatusCode(500);
            }

            if (matchResultDto.CurrentWarshipRating < 0)
            {
                Console.WriteLine($"\n\n\n\n{nameof(matchResultDto.CurrentWarshipRating)} {matchResultDto.CurrentWarshipRating }");
                return StatusCode(500);
            }
            
            return matchResultDto.SerializeToBase64String();
        }
    }
}