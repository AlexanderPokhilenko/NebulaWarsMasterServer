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
        
        [Route(nameof(CreateSmallLootbox))]
        [HttpPost]
        public async Task<ActionResult<string>> CreateSmallLootbox([FromForm] string playerServiceId)
        {
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }

            LootboxModel lootboxModel = await lootboxFacadeService.CreateLootboxModelAsync(playerServiceId);
            return lootboxModel.SerializeToBase64String();
        }
    }
}