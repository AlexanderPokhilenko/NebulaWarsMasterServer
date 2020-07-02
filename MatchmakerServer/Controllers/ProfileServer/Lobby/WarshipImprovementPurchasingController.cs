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
        private readonly WarshipImprovementFacadeService warshipImprovementFacadeService;

        public WarshipImprovementPurchasingController(WarshipImprovementFacadeService warshipImprovementFacadeService)
        {
            this.warshipImprovementFacadeService = warshipImprovementFacadeService;
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

            bool success = await warshipImprovementFacadeService.TryBuyLevel(playerServiceId, warshipId.Value);

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