using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.Shop.Sales;
using AmoebaGameMatcherServer.Services.Shop.ShopModel;
using Microsoft.AspNetCore.Mvc;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    [Route("[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ShopService shopService;
        private readonly SellerService sellerService;

        public ShopController(ShopService shopService, SellerService sellerService)
        {
            this.shopService = shopService;
            this.sellerService = sellerService;
        }
        
        /// <summary>
        /// Создаёт модель магазина для игрока.
        /// </summary>
        [Route(nameof(GetShopModel))]
        [HttpGet]
        public async Task<ActionResult<string>> GetShopModel([FromQuery] string playerId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }

            ShopModel shopModel = null;
            try
            {
                shopModel = await shopService.GetShopModelAsync(playerId);
            }
            catch (Exception e)
            {
                Console.WriteLine("Брошено исключение "+e.Message+" "+e.StackTrace);
            }
            if (shopModel == null)
            {
                return StatusCode(500);
            }
            
            return shopModel.SerializeToBase64String();
        }

        [Route(nameof(BuyProduct))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyProduct([FromForm] string playerId, [FromForm] int productId,
            [FromForm] string base64ProductModel, [FromForm] int shopModelId)
        {
            if (string.IsNullOrEmpty(playerId))
            {
                return BadRequest();
            }
            
            if (productId == default)
            {
                return BadRequest();
            }
            
            if (string.IsNullOrEmpty(base64ProductModel))
            {
                return BadRequest();
            }
            
            if (shopModelId == default)
            {
                return BadRequest();
            }
            
            Console.WriteLine($"{nameof(playerId)} {playerId} {nameof(productId)} {productId}");

            await sellerService.BuyProduct(playerId, productId, base64ProductModel,
                shopModelId);
            return Ok();
        }
    }
}