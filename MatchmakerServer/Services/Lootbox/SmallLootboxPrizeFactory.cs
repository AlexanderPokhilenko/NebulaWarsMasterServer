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

        public SmallLootboxPrizeFactory()
        {
            numberOfPrizeTypes = Enum.GetNames(typeof(ResourceTypeEnum)).Length;
        }
        
        [CanBeNull]
        public ResourceModel Create(List<WarshipDbDto> warships)
        {
            ResourceTypeEnum resourceTypeEnum = (ResourceTypeEnum) random.Next(numberOfPrizeTypes);
            switch (resourceTypeEnum)
            {
                case ResourceTypeEnum.SoftCurrency:
                {
                    int amount = random.Next(15, 100);
                    var model = new SoftCurrencyResourceModel()
                    {
                        Amount = amount
                    };
                    return new ResourceModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        ResourceTypeEnum = ResourceTypeEnum.SoftCurrency
                    };
                }
                case ResourceTypeEnum.WarshipPowerPoints:
                {
                    int warshipIndex = random.Next(warships.Count);
                    var warship = warships[warshipIndex];

                    int amount = random.Next(2, 15);

                    warship.WarshipPowerPoints += amount;
                    var model = new WarshipPowerPointsResourceModel();
                    var test = WarshipPowerScale.GetModel(warship.WarshipPowerLevel);
                    if (test == null)
                    {
                        return null;
                    }
                    model.MaxValueForLevel = test.PowerPointsCost;
                    model.WarshipSkinName = warship.CurrentSkinType.Name;
                    model.FinishValue = warship.WarshipPowerPoints + amount;
                    model.StartValue = warship.WarshipPowerPoints;
                    model.WarshipId = warship.Id;

                    return new ResourceModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        ResourceTypeEnum = ResourceTypeEnum.WarshipPowerPoints
                    };
                }
                case ResourceTypeEnum.HardCurrency:
                {
                    var model = new HardCurrencyResourceModel()
                    {
                        Amount = random.Next(2, 15)
                    };
                    return new ResourceModel
                    {
                        SerializedModel = ZeroFormatterSerializer.Serialize(model),
                        ResourceTypeEnum = ResourceTypeEnum.HardCurrency
                    };
                }
                default:
                    throw new Exception("Неизвестный тип подарка "+resourceTypeEnum);
            }
        }
    }
}