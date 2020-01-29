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
        [HttpDelete]
        public ActionResult DeleteRoom([FromForm]string secretKey, [FromForm] int roomNumber)
        {
            // bool requestCameFromARealGameServer = CheckSecretKey(secretKey);
            // if (!requestCameFromARealGameServer)
            //     return new ForbidResult();

            if(roomNumber == 0)
                return new BadRequestResult();

            gameMatcher.DeleteRoom(roomNumber);
            return Ok();
        }
        
        private bool CheckSecretKey(string secretKey)
        {
            return Globals.SecretKey == secretKey;
        }
    }
}