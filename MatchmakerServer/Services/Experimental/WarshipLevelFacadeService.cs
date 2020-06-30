using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Controllers
{
    public class WarshipLevelFacadeService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly AccountDbReaderService accountDbReaderService;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public WarshipLevelFacadeService(AccountDbReaderService accountDbReaderService,
            WarshipPowerScaleModelStorage warshipPowerScaleModelStorage, ApplicationDbContext dbContext)
        {
            this.accountDbReaderService = accountDbReaderService;
            this.dbContext = dbContext;
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
        }

        public async Task<bool> TryBuyLevel([NotNull] string serviceId, int warshipId)
        {
            //Аккаунт существует?
            var accountDbDto = await accountDbReaderService.ReadAccountAsync(serviceId);
            if (accountDbDto == null)
            {
                throw new Exception("Такого аккаунта не существует");
            }
            
            //Корабль существует?
            var warshipDbDto = accountDbDto.Warships.SingleOrDefault(dto => dto.Id == warshipId);
            if (warshipDbDto == null)
            {
                throw new Exception("Этому аккаунту не принаждлежит этот корабль");
            }

            //Достать цену улучшения
            var improvementModel = warshipPowerScaleModelStorage
                .GetWarshipImprovementModel(warshipDbDto.WarshipPowerLevel);

            if (improvementModel == null)
            {
                Console.WriteLine("У корабля уже максимальный уровень");
                return false;
            }
            
            //Достаточно ресурсов для покупки улучшения?
            if (accountDbDto.SoftCurrency < improvementModel.SoftCurrencyCost)
            {
                Console.WriteLine($"Недостаточно денег у аккаунта {nameof(serviceId)} {serviceId} для " +
                                  $"покупки улучшений {nameof(warshipId)} {warshipId}");
                return false;
            }
            
            //Достаточно очков силы для покупки улучшения
            if (warshipDbDto.WarshipPowerPoints < improvementModel.PowerPointsCost)
            {
                Console.WriteLine("Недостаточно очков силы для улучшения");
                return false;
            }
            
            //Записать транзакцию
            Transaction transaction = new Transaction
            {
                AccountId = accountDbDto.Id,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.WarshipLevel,
                WasShown = false,
                Resources = new List<Resource>
                {
                    new Resource
                    {
                        ResourceTypeId = ResourceTypeEnum.WarshipLevel,
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                                Amount = 1,
                                WarshipId = warshipId
                            }
                        },
                        Decrements = new List<Decrement>
                        {
                            new Decrement
                            {
                                DecrementTypeId = DecrementTypeEnum.SoftCurrency,
                                Amount = improvementModel.SoftCurrencyCost
                            },
                            new Decrement
                            {
                                DecrementTypeId = DecrementTypeEnum.WarshipPowerPoints,
                                Amount = improvementModel.PowerPointsCost,
                                WarshipId = warshipDbDto.Id
                            }
                        }
                    }
                }
            };

            await dbContext.Transactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}