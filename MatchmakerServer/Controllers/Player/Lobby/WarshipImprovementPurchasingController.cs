using System;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для покупки улучшения для корабля
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class WarshipImprovementPurchasingController : ControllerBase
    {
        private readonly WarshipImprovementFacadeService warshipImprovementFacadeService;

        public WarshipImprovementPurchasingController(WarshipImprovementFacadeService warshipImprovementFacadeService)
        {
            this.warshipImprovementFacadeService = warshipImprovementFacadeService;
        }
        
        [Route(nameof(BuyImprovement))]
        [HttpPost]
        public async Task<ActionResult<string>> BuyImprovement([FromForm] string playerServiceId, 
            [FromForm] int? warshipId)
        {
            Console.WriteLine($"{nameof(playerServiceId)} {playerServiceId}");
            if (string.IsNullOrEmpty(playerServiceId) || warshipId==null)
            {
                return BadRequest();
            }

            bool success = await warshipImprovementFacadeService.TryBuyImprovement(playerServiceId, warshipId.Value);

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

    /// <summary>
    /// Нужен для покупки улучшения корабля
    /// </summary>
    public class WarshipImprovementFacadeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public WarshipImprovementFacadeService(ApplicationDbContext dbContext,
            WarshipPowerScaleModelStorage warshipPowerScaleModelStorage)
        {
            this.dbContext = dbContext;
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
        }
        
        public async Task<bool> TryBuyImprovement(string playerServiceId, int warshipId)
        {
            //Аккаунт существует?
            Account account = await dbContext.Accounts
                .Where(account1 => account1.ServiceId == playerServiceId)
                .SingleOrDefaultAsync();
            if (account == null)
            {
                Console.WriteLine("Такого аккаунта не сущуствует");
                return false;
            }
            
            //Корабль существует?
            Warship warship = await dbContext.Warships
                .SingleOrDefaultAsync(model => model.Id == warshipId);
            if (warship == null)
            {
                Console.WriteLine("Такого корабля не существует");
                return false;
            }

            //Корабль принадлежит игроку?
            if (warship.Account.ServiceId != playerServiceId)
            {
                Console.WriteLine("Корабль не принадлежит игроку");
                return false;
            }
            
            //Достаточно денег для покупки улучшения?
            int improvementCost = warshipPowerScaleModelStorage.GetWarshipImprovementCost(warship.PowerLevel);
            if (account.RegularCurrency < improvementCost)
            {
                Console.WriteLine($"Недостаточно денег у аккаунта {nameof(playerServiceId)} {playerServiceId} для " +
                                  $"покупки улучшений {nameof(warshipId)} {warshipId}");
                return false;
            }

            //Достаточно очков силы для покупки улучшения
            int pointsNumber =
                warshipPowerScaleModelStorage.GetNumberOfPointsNeededToPurchaseImprovements(warship.PowerLevel);
            if (warship.PowerPoints < pointsNumber)
            {
                Console.WriteLine("Недостаточно очков силы для улучшения");
                return false;
            }
            
            
            //Повысить уровень
            warship.PowerLevel++;
            //Снять очки силы
            warship.PowerPoints -= pointsNumber;
            //Снять деньги
            await dbContext.WarshipImprovementPurchases.AddAsync(new WarshipImprovementPurchase
            {
                DateTime = DateTime.UtcNow,
                WarshipId = warshipId,
                ObtainedPowerLevel = warship.PowerLevel,
                RegularCurrencyCost = improvementCost
            });
                
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}