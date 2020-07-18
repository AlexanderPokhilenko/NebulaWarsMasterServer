using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Experimental;
using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.AspNetCore.Mvc;

namespace AmoebaGameMatcherServer.Controllers.ProfileServer.Lobby
{
    [Route("[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly PurchasesValidatorService purchasesValidatorService;

        public PurchasesController(PurchasesValidatorService purchasesValidatorService)
        {
            this.purchasesValidatorService = purchasesValidatorService;
        }

        [Route(nameof(Validate))]
        [HttpPost]
        public async Task<ActionResult> Validate([FromForm]string sku, [FromForm]string token)
        {
            Console.WriteLine($"{nameof(sku)} {sku}");
            Console.WriteLine($"{nameof(token)} {token}");
            
            if (string.IsNullOrEmpty(sku))
            {
                Console.WriteLine($"{nameof(sku)} was null");
                return BadRequest();
            }
            
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine($"{nameof(token)} was null");
                return BadRequest();
            }
            
            bool success = await purchasesValidatorService.ValidateAsync(sku, token);
            return success ? Ok() : StatusCode(500);
        }
    }
}