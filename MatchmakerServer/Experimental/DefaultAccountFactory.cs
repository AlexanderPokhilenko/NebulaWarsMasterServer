using System;
using System.Collections.Generic;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services
{
    public static class DefaultAccountFactory
    {
        // private static readonly int defaultPremiumCurrency = 0;
        // private static readonly int defaultRegularCurrency = 150;
        // private static readonly int defaultPointsForBigChest = 100;
        // private static readonly int defaultPointsForSmallChest = 100;
        // private static readonly int defaultWarshipCombatPowerLevel = 1;
        // private static readonly int defaultWarshipCombatPowerValue = 0;
        
        public static Account CreateDefaultAccount(string playerId)
        {
            //TODO добавить немного денег аккаунту
            Account account = new Account
            {
                ServiceId = playerId,
                Username = playerId,
                RegistrationDateTime = DateTime.UtcNow,
                Warships = new List<Warship>
                {
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Hare
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Bird
                    },
                    new Warship
                    {
                        WarshipTypeId = WarshipTypeEnum.Smiley
                    }
                },Transactions = new List<Transaction>()
                {
                    new Transaction()
                    {
                        DateTime = DateTime.UtcNow,
                        TransactionTypeId = TransactionTypeEnum.GameRegistration,
                        WasShown = false,
                        Resources = new List<Resource>()
                        {
                            new Resource()
                            {
                                ResourceTypeId = ResourceTypeEnum.Prize,
                                Increments = new List<Increment>()
                                {
                                    new Increment()
                                    {
                                        IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                                        SoftCurrency = 100
                                    },
                                    new Increment()
                                    {
                                        IncrementTypeId = IncrementTypeEnum.HardCurrency,
                                        HardCurrency = 30
                                    },
                                    new Increment()
                                    {
                                        IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                                        LootboxPoints = 150
                                    }
                                }
                            }
                        }
                    }
                }
            };
            
            return account;
        }
    }
}