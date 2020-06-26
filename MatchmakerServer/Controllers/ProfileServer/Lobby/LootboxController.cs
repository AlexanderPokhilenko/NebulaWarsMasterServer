using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LootboxController:ControllerBase
    {
        private readonly LootboxFacadeService lootboxFacadeService;

        public LootboxController(LootboxFacadeService lootboxFacadeService)
        {
            this.lootboxFacadeService = lootboxFacadeService;
        }
        
        [Route(nameof(BuyLootbox))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyLootbox([FromForm] string playerServiceId)
        {
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }

            LootboxModel lootboxModel = await lootboxFacadeService.CreateLootboxModelAsync(playerServiceId);
            if (lootboxModel == null)
            {
                throw new NullReferenceException(nameof(lootboxModel));
            }
            
            return lootboxModel.SerializeToBase64String();
        }
    }
}