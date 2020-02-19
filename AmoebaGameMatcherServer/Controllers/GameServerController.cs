using System;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;

//TODO добавить secretKey проверку

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameServerController : ControllerBase
    {
        private readonly GameMatcherService gameMatcher;

        public GameServerController(GameMatcherService gameMatcher)
        {
            this.gameMatcher = gameMatcher;
        }

        /// <summary>
        /// Метод вызывается гейм сервером при окончании игровой сессии
        /// </summary>
        [Route("DeleteRoom")]
        [HttpDelete]
        public ActionResult DeleteRoom([FromQuery] int gameRoomId)
        {
            if(gameRoomId == 0)
                return new BadRequestResult();

            gameMatcher.DeleteRoom(gameRoomId);
            return Ok();
        }
    }
}