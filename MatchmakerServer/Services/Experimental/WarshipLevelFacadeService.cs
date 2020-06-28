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
        private readonly DbAccountWarshipsReader dbAccountWarshipsReader;
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public WarshipLevelFacadeService(DbAccountWarshipsReader dbAccountWarshipsReader, 
            WarshipPowerScaleModelStorage warshipPowerScaleModelStorage, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbAccountWarshipsReader = dbAccountWarshipsReader;
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
        }

        public async Task<bool> TryBuyLevel([NotNull] string serviceId, int warshipId)
        {
            //Проверить, что корабль принадлежит этому человеку
            AccountDbDto accountDbDto = await dbAccountWarshipsReader.GetAccountWithWarshipsAsync(serviceId);
            WarshipDbDto warship = accountDbDto.Warships
                .SingleOrDefault(warshipDbDto => warshipDbDto.Id == warshipId);

            if (warship == null)
            {
                Console.WriteLine("Этот корабль не принадлежит аккаунту");
                return false;
            }
        
            //Выяснить стоимость транзакции
            var improvementModel = warshipPowerScaleModelStorage.GetWarshipImprovementModel(warship.WarshipPowerLevel);

            //Проверить, что ресурсов хватает
            if (accountDbDto.SoftCurrency < improvementModel.SoftCurrencyCost)
            {
                Console.WriteLine("Не хватает обычной валюты");
                return false;
            }

            if (warship.WarshipPowerPoints < improvementModel.PowerPointsCost)
            {
                Console.WriteLine("Не хватает очков силы");
                return false;
            }
            
            //Записать транзакцию
            Transaction transaction = new Transaction()
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
                                WarshipId = warship.Id
                            }
                        },
                        Increments = new List<Increment>
                        {
                            new Increment
                            {
                                IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                                Amount = warship.WarshipPowerLevel + 1,
                                WarshipId = warship.Id

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