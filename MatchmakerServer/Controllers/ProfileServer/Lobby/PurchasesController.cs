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
        private readonly OrderConfirmationService orderConfirmationService;
        private readonly PurchasesValidatorService purchasesValidatorService;

        public PurchasesController(PurchasesValidatorService purchasesValidatorService,
            OrderConfirmationService orderConfirmationService)
        {
            this.purchasesValidatorService = purchasesValidatorService;
            this.orderConfirmationService = orderConfirmationService;
        }

        [Route(nameof(Validate))]
        [HttpPost]
        public async Task<ActionResult<string>> Validate([FromForm]string sku, [FromForm]string token)
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
        
            string[] productIdsToConfirm = await purchasesValidatorService.ValidateAsync(sku, token);
            if (productIdsToConfirm == null)
            {
                return Ok();
            }
            return Ok(productIdsToConfirm.SerializeToBase64String());
        }

        [Route(nameof(MarkOrderAsCompleted))]
        [HttpPost]
        public async Task<ActionResult> MarkOrderAsCompleted([FromForm] string serviceId, [FromForm] string sku)
        {
            Console.WriteLine(nameof(MarkOrderAsCompleted));
            if (serviceId == null)
            {
                throw new Exception($"{nameof(serviceId)} is null");
            }
        
            if (sku == null)
            {
                throw new Exception($"{nameof(sku)} is null");
            }

            bool success = await orderConfirmationService.TryConfirmOrderAsync(serviceId, sku);
            Console.WriteLine($"{nameof(success)} {success}");
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