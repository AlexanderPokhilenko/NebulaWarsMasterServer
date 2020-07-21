using System;
using System.Collections.Generic;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using DataLayer.Tables;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;
using ZeroFormatter;

namespace AmoebaGameMatcherServer.Services.Lootbox
{
    /// <summary>
    /// Случайно создёт приз для лутбокса.
    /// </summary>
    public class SmallLootboxPrizeFactory
    {
        private readonly int numberOfPrizeTypes;
        private readonly Random random = new Random();
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public SmallLootboxPrizeFactory(WarshipPowerScaleModelStorage warshipPowerScaleModelStorage)
        {
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
            numberOfPrizeTypes = Enum.GetNames(typeof(LootboxPrizeType)).Length;
        }
        
        [CanBeNull]
        public LootboxPrizeModel Create(List<WarshipDbDto> warships)
        {
            LootboxPrizeType prizeType = (LootboxPrizeType) random.Next(numberOfPrizeTypes);
            switch (prizeType)
            {
                case LootboxPrizeType.SoftCurrency:
                {
                    int amount = random.Next(15, 100);
                    var model = new LootboxSoftCurrencyModel()
                    {
                        Amount = amount
                    };
                    return new LootboxPrizeModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        LootboxPrizeType = LootboxPrizeType.SoftCurrency
                    };
                }
                case LootboxPrizeType.WarshipPowerPoints:
                {
                    int warshipIndex = random.Next(warships.Count);
                    var warship = warships[warshipIndex];

                    int amount = random.Next(2, 15);

                    warship.WarshipPowerPoints += amount;
                    var model = new LootboxWarshipPowerPointsModel();
                    var test = warshipPowerScaleModelStorage
                        .GetWarshipImprovementModel(warship.WarshipPowerLevel);
                    if (test == null)
                    {
                        return null;
                    }
                    model.MaxValueForLevel = test.PowerPointsCost;
                    model.WarshipSkinName = warship.CurrentSkinType.Name;
                    model.FinishValue = warship.WarshipPowerPoints + amount;
                    model.StartValue = warship.WarshipPowerPoints;
                    model.WarshipId = warship.Id;

                    return new LootboxPrizeModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        LootboxPrizeType = LootboxPrizeType.WarshipPowerPoints
                    };
                }
                case LootboxPrizeType.HardCurrency:
                {
                    var model = new LootboxHardCurrencyModel()
                    {
                        Amount = random.Next(2, 15)
                    };
                    return new LootboxPrizeModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        LootboxPrizeType = LootboxPrizeType.HardCurrency
                    };
                }
                default:
                    throw new Exception("Неизвестный тип подарка "+prizeType);
            }
        }
    }
}