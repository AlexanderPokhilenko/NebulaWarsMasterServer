using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

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
        public async Task<ActionResult<string>> CreateSmallLootbox([FromForm]string playerServiceId)
        {
            if (string.IsNullOrEmpty(playerServiceId))
            {
                return BadRequest();
            }

            LootboxData lootboxData = await lootboxFacadeService.TryGetLootboxData(playerServiceId);
            
            return DichSerialize(lootboxData);
        }
        
        private string DichSerialize<T>(T response)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(response);
            string stub = Convert.ToBase64String(data);
            return stub;  
        }
    }
}