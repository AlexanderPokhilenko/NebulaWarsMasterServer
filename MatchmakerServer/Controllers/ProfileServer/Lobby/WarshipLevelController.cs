using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.Experimental;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    /// <summary>
    /// Нужен для получения данных о кораблях, статистике аккаунта.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WarshipLevelController : ControllerBase
    {
        private readonly WarshipImprovementFacadeService warshipImprovementFacadeService;

        public WarshipLevelController(WarshipImprovementFacadeService warshipImprovementFacadeService)
        {
            this.warshipImprovementFacadeService = warshipImprovementFacadeService;
        }

        [Route(nameof(BuyLevel))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyLevel([FromForm] string playerServiceId, [FromForm] int? warshipId)
        {
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }

            if (warshipId == null)
            {
                return BadRequest();
            }
            
            bool success = await warshipImprovementFacadeService.TryBuyLevel(playerServiceId, warshipId.Value);

            if (success)
            {
                return Ok();
            }
            else
            {
                Console.WriteLine("Не удалось купить улучшение для корабля");
                return BadRequest();
            }
        }
    }
}