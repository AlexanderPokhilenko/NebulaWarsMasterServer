using System;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Нужен для покупки улучшения корабля
    /// </summary>
    public class WarshipImprovementFacadeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;
        private readonly AccountDbReaderService accountDbReaderService;

        public WarshipImprovementFacadeService(ApplicationDbContext dbContext,
            WarshipPowerScaleModelStorage warshipPowerScaleModelStorage,
            AccountDbReaderService accountDbReaderService)
        {
            this.dbContext = dbContext;
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
            this.accountDbReaderService = accountDbReaderService;
        }
        
        public async Task<bool> TryBuyImprovement(string playerServiceId, int warshipId)
        {
            Warship warship = await dbContext.Warships
                .Include(warship1 => warship1.Account)
                .SingleOrDefaultAsync(warship1 => warship1.Id == warshipId && warship1.Account.ServiceId==playerServiceId);
            
            if (warship == null)
            {
                Console.WriteLine("Такого корабля не существует");
                return false;
            }

            //
            // //Достаточно денег для покупки улучшения?
            // int improvementCost = warshipPowerScaleModelStorage.GetWarshipImprovementCost(warship.WarshipPowerLevel);
            // if (accountDbDto.SoftCurrency < improvementCost)
            // {
            //     Console.WriteLine($"Недостаточно денег у аккаунта {nameof(playerServiceId)} {playerServiceId} для " +
            //                       $"покупки улучшений {nameof(warshipId)} {warshipId}");
            //     return false;
            // }
            //
            // //Достаточно очков силы для покупки улучшения
            // int pointsNumber = warshipPowerScaleModelStorage
            //     .GetNumberOfPointsNeededToPurchaseImprovements(warship.WarshipPowerLevel);
            // if (warship.PowerPointsCost < pointsNumber)
            // {
            //     Console.WriteLine("Недостаточно очков силы для улучшения");
            //     return false;
            // }
            //
            // //Записать транзакцию
            // await dbContext.WarshipPowerPoints.AddAsync(new WarshipImprovementPurchase
            // {
            //     DateTime = DateTime.UtcNow,
            //     WarshipId = warshipId,
            //     ObtainedPowerLevel = warship.WarshipPowerLevel,
            //     RegularCurrencyCost = improvementCost,
            //     SpentPowerPoints = pointsNumber
            // });
            //     
            // await dbContext.SaveChangesAsync();
            return true;
        }
    }
}