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
    public class LootboxResourcesFactory
    {
        private readonly Random random;
        private readonly LootboxResourceTypeFactory lootboxResourceTypeFactory;

        public LootboxResourcesFactory(LootboxResourceTypeFactory lootboxResourceTypeFactory)
        {
            random = new Random();
            this.lootboxResourceTypeFactory = lootboxResourceTypeFactory;
        }

        [CanBeNull]
        public ResourceModel Create(List<WarshipDbDto> warships)
        {
            ResourceTypeEnum resourceTypeEnum = lootboxResourceTypeFactory.CreateResourceType();
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
                    WarshipDbDto warship = warships[warshipIndex];

                    int amount = random.Next(2, 15);

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
                    model.WarshipTypeEnum = warship.WarshipTypeId;

                    warship.WarshipPowerPoints += amount;
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