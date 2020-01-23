using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameMatcherController : ControllerBase
    {
        private readonly GameMatcherService gameMatcher;

        public GameMatcherController(GameMatcherService gameMatcher)
        {
            this.gameMatcher = gameMatcher;
        }

        [HttpPost]
        public ActionResult<string> RegisterPlayer([FromForm]string playerId)
        {
            gameMatcher.RegisterPlayer(playerId);
            return Ok();
        }
        
        [HttpGet]
        public ActionResult<string> GetGameRoomData(string playerId)
        {
            var roomData = gameMatcher.GetGameRoomData(playerId);
            if (roomData == null)
                return "Об этом игроке нет информации";

            return "Есть данные об этом игроке";
            
            //TODO сериализовать данные о комнате
            return Ok();
        }
    }
}