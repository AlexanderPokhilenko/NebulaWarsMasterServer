using AmoebaGameMatcherServer.Utils;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameUpdatesController : ControllerBase
    {
        [Route(nameof(GetGameVersion))]
        [HttpPost]
        public ActionResult<int> GetGameVersion()
        {
            return Globals.GameVersionNumber;
        }
    }
    
    [Route("[controller]")]
    [ApiController]
    public class BattleResultController : ControllerBase
    {
        // [Route(nameof(GetBattleResult))]
        // [HttpPost]
        // public ActionResult<string> GetBattleResult([FromForm] string playerId, [FromForm] int battleId)
        // {
        //     
        // }
    }
}