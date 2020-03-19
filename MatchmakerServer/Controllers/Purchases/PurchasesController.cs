using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services;
using DataLayer;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
    private readonly PurchasesValidatorService purchasesValidatorService;
    private readonly IpAppProductsService ipAppProductsService;

    public PurchasesController(PurchasesValidatorService purchasesValidatorService, IpAppProductsService ipAppProductsService)
    {
        this.purchasesValidatorService = purchasesValidatorService;
        this.ipAppProductsService = ipAppProductsService;
    }

    [Route(nameof(Validate))]
    [HttpPost]
    public ActionResult Validate([FromForm]string productId, [FromForm]string token)
    {
        Console.WriteLine($"{nameof(Validate)} вызван");
        Console.WriteLine("данные начало");
        Console.WriteLine($"{nameof(productId)} {productId}");
        Console.WriteLine($"{nameof(token)} {token}");
        Console.WriteLine("данные конец");
        
        //TODO это костыль
        if (!productId.Contains('.'))
        {
            productId = GoogleApiGlobals.PackageName + "." + productId;
            Console.WriteLine($"{nameof(productId)} был изменён на {productId}");
        }

        purchasesValidatorService.Validate(productId, token);
        return Ok();
    }
    
    [Route(nameof(LogInAppProducts))]
    [HttpPost]
    public async Task<ActionResult> LogInAppProducts()
    {
        await ipAppProductsService.GetInAppProducts();
        return Ok();
    }
}