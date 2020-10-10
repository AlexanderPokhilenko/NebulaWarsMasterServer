using System;
using System.Collections.Generic;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Experimental
{
    /// <summary>
    /// Создаёт транзакции для заполенния пустого аккаунта.
    /// </summary>
    public class DefaultAccountTransactionsFactory
    {
        public Transaction CreateResourcesTransaction(Account account)
        {
            return new Transaction
            {
                AccountId = account.Id,
                DateTime = DateTime.UtcNow,
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                        Amount = 300
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.HardCurrency,
                        Amount = 15
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                        Amount = 100 * 7
                    }
                }
            };
        }
        public Transaction WarshipLevelsTransaction(Account account)
        {
            //Присвоить кораблям первый уровень
            Transaction warshipsLevelTransaction = new Transaction
            {
                AccountId = account.Id,
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[0].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[1].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[2].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = account.Warships[3].Id
                    }
                }
            };
            return warshipsLevelTransaction;
        }
        public Transaction CreateSkinsForWarships(Account account)
        {
             
            //Добавить кораблям скины
            Transaction skinTransaction = new Transaction
            {
                AccountId = account.Id,
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Hare,
                        WarshipId = account.Warships[0].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Bird,
                        WarshipId = account.Warships[1].Id
                    },   
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Raven,
                        WarshipId = account.Warships[1].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Smiley,
                        WarshipId = account.Warships[2].Id
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Skin,
                        Amount = 1,
                        SkinTypeId = SkinTypeEnum.Sage,
                        WarshipId = account.Warships[3].Id
                    }
                }
            };
            return skinTransaction;
        }
        public Transaction CreateWarshipsPowerPoints(Account account)
        {
            //Добавить кораблям очки силы
            Transaction powerPointsTransaction = new Transaction
            {
                TransactionTypeId = TransactionTypeEnum.GameRegistration,
                AccountId = account.Id,
                DateTime = DateTime.UtcNow,
                WasShown = false,
                Increments = new List<Increment>()
                {
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[0].Id,
                        Amount = 0
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[1].Id,
                        Amount = 0
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[2].Id,
                        Amount = 0
                    },
                    new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        WarshipId = account.Warships[3].Id,
                        Amount = 0
                    }
                }
            };

            return powerPointsTransaction;
        }
    }
}