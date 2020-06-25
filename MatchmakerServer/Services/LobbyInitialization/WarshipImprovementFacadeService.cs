// using System;
// using System.Linq;
// using System.Threading.Tasks;
// using AmoebaGameMatcherServer.Services.LobbyInitialization;
// using DAL;
// using DAL.Tables;
// using Microsoft.EntityFrameworkCore;
// using NetworkLibrary.NetworkLibrary.Http;
//
// namespace AmoebaGameMatcherServer.Controllers
// {
//     /// <summary>
//     /// Нужен для покупки улучшения корабля
//     /// </summary>
//     public class WarshipImprovementFacadeService
//     {
//         private readonly ApplicationDbContext dbContext;
//         private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;
//         private readonly AccountDbReaderService accountDbReaderService;
//
//         public WarshipImprovementFacadeService(ApplicationDbContext dbContext,
//             WarshipPowerScaleModelStorage warshipPowerScaleModelStorage,
//             AccountDbReaderService accountDbReaderService)
//         {
//             this.dbContext = dbContext;
//             this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
//             this.accountDbReaderService = accountDbReaderService;
//         }
//         
//         public async Task<bool> TryBuyImprovement(string playerServiceId, int warshipId)
//         {
//             //Аккаунт существует?
//             Account account = await accountDbReaderService.ReadAccountAsync(playerServiceId);
//             
//             if (account == null)
//             {
//                 Console.WriteLine("Такого аккаунта не сущуствует");
//                 return false;
//             }
//             
//             //Корабль существует?
//             Warship warship = await dbContext.Warships
//                 .SingleOrDefaultAsync(model => model.Id == warshipId);
//             if (warship == null)
//             {
//                 Console.WriteLine("Такого корабля не существует");
//                 return false;
//             }
//
//             //Корабль принадлежит игроку?
//             if (warship.Account.ServiceId != playerServiceId)
//             {
//                 Console.WriteLine("Корабль не принадлежит игроку");
//                 return false;
//             }
//             
//             //Достаточно денег для покупки улучшения?
//             int improvementCost = warshipPowerScaleModelStorage.GetWarshipImprovementCost(warship.PowerLevel);
//             if (account.SoftCurrency < improvementCost)
//             {
//                 Console.WriteLine($"Недостаточно денег у аккаунта {nameof(playerServiceId)} {playerServiceId} для " +
//                                   $"покупки улучшений {nameof(warshipId)} {warshipId}");
//                 return false;
//             }
//
//             //Достаточно очков силы для покупки улучшения
//             int pointsNumber =
//                 warshipPowerScaleModelStorage.GetNumberOfPointsNeededToPurchaseImprovements(warship.PowerLevel);
//             if (warship.PowerPoints < pointsNumber)
//             {
//                 Console.WriteLine("Недостаточно очков силы для улучшения");
//                 return false;
//             }
//      
//             //Записать транзакцию
//             await dbContext.WarshipPowerPoints.AddAsync(new WarshipImprovementPurchase
//             {
//                 DateTime = DateTime.UtcNow,
//                 WarshipId = warshipId,
//                 ObtainedPowerLevel = warship.PowerLevel,
//                 RegularCurrencyCost = improvementCost,
//                 SpentPowerPoints = pointsNumber
//             });
//                 
//             await dbContext.SaveChangesAsync();
//             return true;
//         }
//     }
// }