using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Controllers;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.GoogleApi;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

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
        
        string[] productIdsToConfirm = await purchasesValidatorService.Validate(productId, token);
        if (productIdsToConfirm == null)
        {
            return Ok();
        }
        return Ok(productIdsToConfirm.SerializeToBase64String());
    }
}