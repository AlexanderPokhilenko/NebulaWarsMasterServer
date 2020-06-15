using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.GoogleApi;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly PurchasesValidatorService purchasesValidatorService;
    private readonly OrderConfirmationService orderConfirmationService;

    public PurchasesController(PurchasesValidatorService purchasesValidatorService,
        OrderConfirmationService orderConfirmationService)
    {
        this.purchasesValidatorService = purchasesValidatorService;
        this.orderConfirmationService = orderConfirmationService;
    }

    [Route(nameof(Validate))]
    [HttpPost]
    public async Task<ActionResult<string>> Validate([FromForm]string productId, [FromForm]string token)
    {
        Console.WriteLine($"{nameof(productId)} {productId}");
        Console.WriteLine($"{nameof(token)} {token}");
        
        if (string.IsNullOrEmpty(productId))
        {
            Console.WriteLine($"{nameof(productId)} was null");
            return BadRequest();
        }
        
        if (string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"{nameof(token)} was null");
            return BadRequest();
        }
        
        string[] productIdsToConfirm = await purchasesValidatorService.ValidateAsync(productId, token);
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