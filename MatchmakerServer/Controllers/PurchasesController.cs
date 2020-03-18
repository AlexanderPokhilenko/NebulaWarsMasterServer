using System;
using AmoebaGameMatcherServer.Services;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly GooglePurchasesWrapperService googlePurchasesWrapperService;

    public PurchasesController(GooglePurchasesWrapperService googlePurchasesWrapperService)
    {
        this.googlePurchasesWrapperService = googlePurchasesWrapperService;
    }

    [Route(nameof(Validate))]
    [HttpPost]
    public ActionResult Validate([FromForm]string productId, [FromForm]string token)
    {
        Console.WriteLine($"{nameof(productId)} {productId}");
        Console.WriteLine($"{nameof(token)} {token}");
        
        googlePurchasesWrapperService.Validate(productId, token);
        return Ok();
    }
}