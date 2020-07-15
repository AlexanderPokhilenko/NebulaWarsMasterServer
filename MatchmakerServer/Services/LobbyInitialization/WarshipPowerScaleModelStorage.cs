using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Хранит цену для перехода на новый уровень для всех уровней
    /// </summary>
    public class WarshipPowerScaleModelStorage    
    {
        private readonly WarshipPowerScaleModel warshipPowerScaleModel = new WarshipPowerScaleModel
        {
            PowerLevelModels = new[]
            {
                new WarshipImprovementModel {PowerPointsCost = 0,    SoftCurrencyCost = 0},
                new WarshipImprovementModel {PowerPointsCost = 20,    SoftCurrencyCost = 20},
                new WarshipImprovementModel {PowerPointsCost = 30,    SoftCurrencyCost = 35},
                new WarshipImprovementModel {PowerPointsCost = 50,    SoftCurrencyCost = 75},
                new WarshipImprovementModel {PowerPointsCost = 80,    SoftCurrencyCost = 140},
                new WarshipImprovementModel {PowerPointsCost = 130,   SoftCurrencyCost = 290},
                new WarshipImprovementModel {PowerPointsCost = 210,   SoftCurrencyCost = 480},
                new WarshipImprovementModel {PowerPointsCost = 340,   SoftCurrencyCost = 800},
                new WarshipImprovementModel {PowerPointsCost = 550,   SoftCurrencyCost = 1250}
            }
        };
        
        public WarshipPowerScaleModel Create()
        {
            return warshipPowerScaleModel;
        }

        [CanBeNull]
        public WarshipImprovementModel GetWarshipImprovementModel(int powerLevel)
        {
            if (warshipPowerScaleModel.PowerLevelModels.Length < powerLevel)
            {
                return null;
            }
            else
            {
                return warshipPowerScaleModel.PowerLevelModels[powerLevel];
            }
        }
    }
}