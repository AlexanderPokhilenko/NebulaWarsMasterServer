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
        
        // //TODO это костыль
        // //Превращает no_ads => com.tikaytech.nebulaWars.no_ads
        // if (!productId.Contains('.'))
        // {
        //     productId = GoogleApiGlobals.PackageName + "." + productId;
        //     Console.WriteLine($"{nameof(productId)} был изменён на {productId}");
        // }
        
        purchasesValidatorService.Validate(productId, token);
        purchasesValidatorService.Validate(GoogleApiGlobals.PackageName + "." +productId, token);
        return Ok();
    }
    
    [Route(nameof(ValidateTest))]
    [HttpPost]
    public ActionResult ValidateTest(string productId, string token)
    {
        Console.WriteLine($"{nameof(Validate)} вызван");
        Console.WriteLine("данные начало");
        Console.WriteLine($"{nameof(productId)} {productId}");
        Console.WriteLine($"{nameof(token)} {token}");
        Console.WriteLine("данные конец");

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
        
        // //TODO это костыль
        // //Превращает no_ads => com.tikaytech.nebulaWars.no_ads
        // if (!productId.Contains('.'))
        // {
        //     productId = GoogleApiGlobals.PackageName + "." + productId;
        //     Console.WriteLine($"{nameof(productId)} был изменён на {productId}");
        // }

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