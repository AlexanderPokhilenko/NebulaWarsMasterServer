using System;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameServerController : ControllerBase
    {
        /// <summary>
        /// Метод вызывается гейм сервером при окончании игровой сессии.
        /// </summary>
        [Route(nameof(DeleteRoom))]
        [HttpDelete]
        public ActionResult DeleteRoom([FromQuery] int matchId)
        {
            if(matchId == 0)
                return new BadRequestResult();

            //TODO сделать запись об окончании боя
            
            // gameMatcher.DeleteRoom(gameRoomId);
            return Ok();
        }
        
        /// <summary>
        /// Метод вызывается гейм сервером при смерти игрока.
        /// </summary>
        [Route(nameof(PlayerDeath))]
        [HttpDelete]
        public ActionResult PlayerDeath([FromQuery] int playerId, [FromQuery] int placeInBattle)
        {
            Console.WriteLine($"test {nameof(playerId)} {playerId} {nameof(placeInBattle)} {placeInBattle}");
            
            //TODO дописать результат боя игрока в бд

            return Ok();
        }
    }
}