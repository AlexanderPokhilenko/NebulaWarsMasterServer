using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameMatcherController : ControllerBase
    {
        private GameMatcherService gameMatcher;

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
        public async Task<ActionResult<string>> GetGameRoomData(string playerId)
        {
            var roomData = await gameMatcher.GetGameRoomData(playerId);
            if (roomData == null)
                return StatusCode(500);
            
            return Ok();
        }
    }
}