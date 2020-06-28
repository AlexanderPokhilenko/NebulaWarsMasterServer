using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для получения данных о кораблях, статистике аккаунта.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WarshipLevelController : ControllerBase
    {
        private readonly WarshipLevelFacadeService warshipLevelFacadeService;

        public WarshipLevelController(WarshipLevelFacadeService warshipLevelFacadeService)
        {
            this.warshipLevelFacadeService = warshipLevelFacadeService;
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
            
            bool success = await warshipLevelFacadeService.TryBuyLevel(playerServiceId, warshipId.Value);

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