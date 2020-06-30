using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для покупки улучшения для корабля
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WarshipImprovementPurchasingController : ControllerBase
    {
        private readonly WarshipLevelFacadeService warshipLevelFacadeService;

        public WarshipImprovementPurchasingController(WarshipLevelFacadeService warshipLevelFacadeService)
        {
            this.warshipLevelFacadeService = warshipLevelFacadeService;
        }
        
        [Route(nameof(BuyImprovement))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyImprovement([FromForm] string playerServiceId, 
            [FromForm] int? warshipId)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId) || warshipId == null)
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
                return BadRequest();
            }
        }
    }
}