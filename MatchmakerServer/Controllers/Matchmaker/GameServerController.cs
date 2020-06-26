using System;
using System.Threading.Channels;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.MatchFinishing;
using AmoebaGameMatcherServer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameServerController : ControllerBase
    {
        private readonly BattleRoyaleMatchFinisherService battleRoyaleMatchFinisher;

        public GameServerController(BattleRoyaleMatchFinisherService battleRoyaleMatchFinisher)
        {
            this.battleRoyaleMatchFinisher = battleRoyaleMatchFinisher;
        }
        
        [Route(nameof(PlayerDeath))]
        [HttpDelete]
        public async Task<ActionResult> PlayerDeath([FromQuery] int? accountId, [FromQuery] int? placeInBattle, 
            [FromQuery] int? matchId, string secret)
        {
            if (accountId == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(accountId)} is null");
                return BadRequest();
            }

            if (placeInBattle == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(placeInBattle)} is null");
                return BadRequest();
            }
            
            if (matchId == null)
            {
                Console.WriteLine($"{nameof(PlayerDeath)} {nameof(matchId)} is null");
                return BadRequest();
            }
            
            if (secret != Globals.GameServerSecret)
            {
                return BadRequest();
            }

            bool succes = await battleRoyaleMatchFinisher
                .UpdatePlayerMatchResultInDbAsync(accountId.Value, placeInBattle.Value, matchId.Value);
            
            return Ok();
        }
        
        [Route(nameof(DeleteMatch))]
        [HttpDelete]
        public async Task<ActionResult> DeleteMatch([FromQuery] int? matchId, string secret)
        {
            if (matchId == null)
            {
                return BadRequest();
            }

            if (secret != Globals.GameServerSecret)
            {
                return BadRequest();
            }
            
            await battleRoyaleMatchFinisher.FinishMatchAndWriteToDbAsync(matchId.Value);
            return Ok();
        }
    }
}