// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using DataLayer;
// using DataLayer.Tables;
// using Microsoft.EntityFrameworkCore;
// using NetworkLibrary.NetworkLibrary.Http;
//
// namespace AmoebaGameMatcherServer.Controllers
// {
//     /// <summary>
//     /// Снимает со счёта игрока стоимость лутбокса. Сохраняет награды.
//     /// </summary>
//     public class LootboxDbWriterService
//     {
//         private readonly ApplicationDbContext dbContext;
//
//         public LootboxDbWriterService(ApplicationDbContext dbContext)
//         {
//             this.dbContext = dbContext;
//         }
//         
//         public async Task Write(string playerServiceId, LootboxModel lootboxModel)
//         {
//             Account account = await dbContext.Accounts
//                 .Where(account1 => account1.ServiceId == playerServiceId)
//                 .SingleOrDefaultAsync();
//
//             account.PointsForSmallLootbox -= 100;
//
//             LootboxDb lootboxDb = new LootboxDb
//             {
//                 LootboxType = LootboxType.Small,
//                 LootboxPrizePointsForSmallLootboxes = new List<LootboxPrizePointsForSmallLootbox>(),
//                 LootboxPrizeRegularCurrencies = new List<LootboxPrizeRegularCurrency>(),
//                 LootboxPrizeWarshipPowerPoints = new List<LootboxPrizeWarshipPowerPoints>(),
//                 AccountId = account.Id,
//                 CreationDate = DateTime.UtcNow,
//                 WasShown = false
//             };
//
//             foreach (LootboxPrizeModel prize in lootboxModel.Prizes)
//             {
//                 switch (prize.LootboxPrizeType)
//                 {
//                     case LootboxPrizeType.RegularCurrency:
//                         lootboxDb.LootboxPrizeRegularCurrencies.Add(new LootboxPrizeRegularCurrency
//                         {
//                             Quantity = prize.Quantity
//                         });
//                         break;
//                     case LootboxPrizeType.PointsForSmallLootbox:
//                         lootboxDb.LootboxPrizePointsForSmallLootboxes.Add(new LootboxPrizePointsForSmallLootbox()
//                         {
//                             Quantity = prize.Quantity
//                         });
//                         break;
//                     case LootboxPrizeType.WarshipPowerPoints:
//                         if (prize.WarshipId != null)
//                         {
//                             var lootboxPrizeWarshipPowerPoints = new LootboxPrizeWarshipPowerPoints
//                             {
//                                 Quantity = prize.Quantity,
//                                 WarshipId = prize.WarshipId.Value
//                             };
//                             lootboxDb.LootboxPrizeWarshipPowerPoints.Add(lootboxPrizeWarshipPowerPoints);
//                         }
//                         else
//                         {
//                             throw new Exception($"Не установлен {nameof(prize.WarshipId)}");
//                         }
//                         break;
//                     default:
//                         throw new ArgumentOutOfRangeException();
//                 }
//             }
//
//             await dbContext.Lootbox.AddAsync(lootboxDb);
//             await dbContext.SaveChangesAsync();
//         }
//     }
// }