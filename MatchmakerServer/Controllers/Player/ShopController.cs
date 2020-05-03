using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopFacadeService shopFacadeService;

        public ShopController(ShopFacadeService shopFacadeService)
        {
            this.shopFacadeService = shopFacadeService;
        }
        
        /// <summary>
        /// Возвращает данные для окна покупок. Для каждого игрока окно покупок может быть уникальным.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        [Route(nameof(GetShopData))]
        [HttpPost]
        public async Task<ActionResult<string>> GetShopData([FromForm] string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }

            var shopModel = await shopFacadeService.GetShopModel(playerId);
            if (shopModel == null)
            {
                return StatusCode(500);
            }
            
            return DichSerialize(shopModel);
        }

        [Route(nameof(BuyProduct))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyProduct([FromForm] string playerId, string productId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }
            
            return Ok();
        }
        
        private string DichSerialize<T>(T response)
        {
            byte[] data = ZeroFormatterSerializer.Serialize(response);
            string stub = Convert.ToBase64String(data);
            return stub;  
        }
    }
}