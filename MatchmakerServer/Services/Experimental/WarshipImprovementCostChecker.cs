using System;
using AmoebaGameMatcherServer.Services.LobbyInitialization;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Experimental
{
    public enum FaultReason
    {
        InsufficientSoftCurrency,
        InsufficientWarshipPowerPoints,
        MaximumLevelAlreadyReached
    }
    public class WarshipImprovementCostChecker
    {
        private readonly WarshipPowerScaleModelStorage warshipPowerScaleModelStorage;

        public WarshipImprovementCostChecker(WarshipPowerScaleModelStorage warshipPowerScaleModelStorage)
        {
            this.warshipPowerScaleModelStorage = warshipPowerScaleModelStorage;
        }

        public bool CanAPurchaseBeMade(int softCurrency, int warshipPowerLevel,  int warshipPowerPoints,
            out FaultReason? faultReason)
        {
            Console.WriteLine($"{nameof(warshipPowerLevel)} {warshipPowerLevel} {nameof(warshipPowerPoints)} {warshipPowerPoints}");
            //Достать цену улучшения
            WarshipImprovementModel improvementModel = warshipPowerScaleModelStorage
                .GetWarshipImprovementModel(warshipPowerLevel);

            if (improvementModel == null)
            {
                Console.WriteLine("У корабля уже максимальный уровень");
                faultReason = FaultReason.MaximumLevelAlreadyReached;
                return false;
            }

            if (softCurrency < improvementModel.SoftCurrencyCost)
            {
                faultReason = FaultReason.InsufficientSoftCurrency;
                return false;
            }
            
            if (warshipPowerPoints < improvementModel.PowerPointsCost)
            {
                faultReason = FaultReason.InsufficientWarshipPowerPoints;
                return false;
            }

            faultReason = null;
            return true;
        }


        public WarshipImprovementModel GetImprovementModel(int warshipPowerLevel)
        {
            WarshipImprovementModel improvementModel = warshipPowerScaleModelStorage
                .GetWarshipImprovementModel(warshipPowerLevel);
            return improvementModel;
        }
    }
}