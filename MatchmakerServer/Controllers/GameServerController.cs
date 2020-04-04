using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameServerController : ControllerBase
    {
        private readonly BattleRoyaleMatchFinisherService matchFinisherService;

        public GameServerController(BattleRoyaleMatchFinisherService matchFinisherService)
        {
            this.matchFinisherService = matchFinisherService;
        }
        
        [Route(nameof(DeleteRoom))]
        [HttpDelete]
        public async Task<ActionResult> DeleteRoom([FromQuery] int? matchId)
        {
            Console.WriteLine($"\n{nameof(DeleteRoom)}\n");
            if (matchId == null)
            {
                Console.WriteLine($"{nameof(matchId)} is null");
                return new BadRequestResult();
            }
            await matchFinisherService.DeleteRoom(matchId.Value);
            
            return Ok();
        }
        
        [Route(nameof(PlayerDeath))]
        [HttpDelete]
        public async Task<ActionResult> PlayerDeath([FromQuery] int? accountId, [FromQuery] int? placeInBattle, [FromQuery] int? matchId)
        {
            if (accountId == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(accountId)} is null");
                return StatusCode(500);
            }

            if (placeInBattle == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(placeInBattle)} is null");
                return StatusCode(500);
            }
            
            if (matchId == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(matchId)} is null");
                return StatusCode(500);
            }

            await matchFinisherService.PlayerDeath(accountId.Value, placeInBattle.Value, matchId.Value);

            Console.WriteLine($"{nameof(PlayerDeath)} Успешная запись в БД");
            return Ok();
        }
    }
}