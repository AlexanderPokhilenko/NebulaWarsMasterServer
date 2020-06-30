using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
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
                new WarshipPowerLevelModel {PowerPointsCost = 0,    SoftCurrencyCost = 0},
                new WarshipPowerLevelModel {PowerPointsCost = 20,    SoftCurrencyCost = 20},
                new WarshipPowerLevelModel {PowerPointsCost = 30,    SoftCurrencyCost = 35},
                new WarshipPowerLevelModel {PowerPointsCost = 50,    SoftCurrencyCost = 75},
                new WarshipPowerLevelModel {PowerPointsCost = 80,    SoftCurrencyCost = 140},
                new WarshipPowerLevelModel {PowerPointsCost = 130,   SoftCurrencyCost = 290},
                new WarshipPowerLevelModel {PowerPointsCost = 210,   SoftCurrencyCost = 480},
                new WarshipPowerLevelModel {PowerPointsCost = 340,   SoftCurrencyCost = 800},
                new WarshipPowerLevelModel {PowerPointsCost = 550,   SoftCurrencyCost = 1250}
            }
        };
        
        public WarshipPowerScaleModel Create()
        {
            return warshipPowerScaleModel;
        }

        [CanBeNull]
        public WarshipPowerLevelModel GetWarshipImprovementModel(int powerLevel)
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