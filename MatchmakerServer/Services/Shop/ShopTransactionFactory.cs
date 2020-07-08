using System;
using System.Collections.Generic;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class ShopTransactionFactory
    {
        public Transaction Create(ProductModel productModel, int accountId)
        {
            if (productModel.Disabled)
            {
                throw new Exception("Этот продукт уже был куплен.");
            }
            

            List<Increment> increments = CreateIncrements(productModel);
            Decrement decrement = CreateDecrement(productModel);

            if (increments == null || increments.Count == 0)
            {
                throw new Exception("При покупке ничего не добавляется");
            }


            Transaction transaction = new Transaction
            {
                AccountId = accountId,
                WasShown = false,
                DateTime = DateTime.UtcNow,
                Increments = increments,
                TransactionTypeId = productModel.TransactionType,
                Decrements = new List<Decrement> {decrement}
            };
            
            
            
            return transaction;
        }

        [CanBeNull]
        private Decrement CreateDecrement(ProductModel productModel)
        {
            Decrement decrement;

            switch (productModel.CurrencyType)
            {
                case CurrencyType.SoftCurrency:
                    decrement = new Decrement
                    {
                        DecrementTypeId = DecrementTypeEnum.SoftCurrency,
                        Amount = productModel.Cost
                    };
                    break;
                case CurrencyType.HardCurrency:
                    decrement = new Decrement
                    {
                        DecrementTypeId = DecrementTypeEnum.HardCurrency,
                        Amount = productModel.Cost
                    };
                    break;
                case CurrencyType.RealCurrency:
                    throw new Exception("Эта покупка не должна тут обрабатываться");
                case CurrencyType.Free:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return decrement;
        }

        private List<Increment> CreateIncrements(ProductModel productModel)
        {
            List<Increment> increments = new List<Increment>();
            switch (productModel.TransactionType)
            {
                case TransactionTypeEnum.Lootbox:
                {
                    Increment increment = new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                        Amount = 100
                    };
                    increments.Add(increment);
                    break;
                }
                case TransactionTypeEnum.LootboxSet:
                {
                    if (productModel.MagnificationRatio != null)
                    {
                        Increment increment = new Increment
                        {
                            IncrementTypeId = IncrementTypeEnum.LootboxPoints,
                            Amount = 100*productModel.MagnificationRatio.Value
                        };
                        increments.Add(increment);
                    }
                    else
                    {
                        throw new Exception("Набор лутбоксов не содержит кол-ва лутбоксов");
                    }

                    break;
                }
                case TransactionTypeEnum.Warship:
                {
                    Increment increment = new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.Warship,
                        Amount = 0,
                        WarshipId = productModel.WarshipModel.WarshipId
                    };
                    increments.Add(increment);
                    break;
                }
                case TransactionTypeEnum.WarshipAndSkin:
                {
                    throw new NotImplementedException();
                    // Increment incrementWarship = new Increment
                    // {
                    //     IncrementTypeId = IncrementTypeEnum.Warship,
                    //     Amount = 0,
                    //     WarshipId = productModel.WarshipModel.WarshipId
                    // };
                    // Increment incrementSkin = new Increment
                    // {
                    //     IncrementTypeId = IncrementTypeEnum.Skin,
                    //     Amount = 0,
                    //     WarshipId = productModel.WarshipModel.WarshipId,
                    //     SkinPrefabPath = productModel.SkinPrefabPath
                    // };
                    // increments.Add(incrementWarship);
                    // increments.Add(incrementSkin);
                    // break;
                }
                case TransactionTypeEnum.Skin:
                {
                    throw new NotImplementedException();
                    // Increment incrementSkin = new Increment
                    // {
                    //     IncrementTypeId = IncrementTypeEnum.Skin,
                    //     Amount = 0,
                    //     WarshipId = productModel.WarshipModel.WarshipId,
                    //     SkinPrefabPath = productModel.SkinPrefabPath
                    // };
                    // increments.Add(incrementSkin);
                    // break;
                }
                case TransactionTypeEnum.WarshipPowerPoints:
                {
                    Increment increment = new Increment
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipPowerPoints,
                        Amount = productModel.Amount,
                        WarshipId = productModel.WarshipModel.WarshipId
                    };
                    increments.Add(increment);
                    break;
                }
                case TransactionTypeEnum.DailyPrize:
                {
                    throw new NotImplementedException();
                }
                case TransactionTypeEnum.GameRegistration:
                {
                    throw new NotImplementedException();
                }
                case TransactionTypeEnum.SoftCurrency:
                {
                    Increment increment = new Increment()
                    {
                        IncrementTypeId = IncrementTypeEnum.SoftCurrency,
                        Amount = productModel.Amount,
                        WarshipId = productModel.WarshipModel.WarshipId
                    };
                    increments.Add(increment);
                    break;
                }
                case TransactionTypeEnum.HardCurrency:
                {
                     throw new Exception("Покупка премиум валюты не должна осуществляться тут");
                }
                case TransactionTypeEnum.WarshipLevel:
                {
                    Increment increment = new Increment()
                    {
                        IncrementTypeId = IncrementTypeEnum.WarshipLevel,
                        Amount = 1,
                        WarshipId = productModel.WarshipModel.WarshipId
                    };
                    increments.Add(increment);
                    break;
                }
                case TransactionTypeEnum.MatchReward:
                    throw new Exception("Награда за бой не должна тут обрабатываться");
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return increments;
        }
    }
}