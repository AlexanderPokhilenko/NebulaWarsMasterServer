using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    /// <summary>
    /// Нужен для сохранения текущего скина
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class SkinController : ControllerBase
    {
        private readonly CurrentSkinChangingService currentSkinChangingService;

        public SkinController(CurrentSkinChangingService currentSkinChangingService)
        {
            this.currentSkinChangingService = currentSkinChangingService;
        }

        [Route(nameof(Change))]
        [HttpPost]
        public async Task<ActionResult<string>> Change([FromForm] string playerServiceId, [FromForm] int warshipId,
            [FromForm] string skinName)
        {
            bool success = await currentSkinChangingService.TryChange(playerServiceId, warshipId, skinName);
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}