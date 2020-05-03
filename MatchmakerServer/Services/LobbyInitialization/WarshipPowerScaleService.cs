using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    public class WarshipPowerScaleService
    {
        private readonly WarshipPowerScaleModel warshipPowerScaleModel = new WarshipPowerScaleModel()
        {
            PowerLevelModels = new[]
            {
                new WarshipPowerLevelModel {PowerPoints = 0,    Cost = 0},
                new WarshipPowerLevelModel {PowerPoints = 20,    Cost = 20},
                new WarshipPowerLevelModel {PowerPoints = 30,    Cost = 35},
                new WarshipPowerLevelModel {PowerPoints = 50,    Cost = 75},
                new WarshipPowerLevelModel {PowerPoints = 80,    Cost = 140},
                new WarshipPowerLevelModel {PowerPoints = 130,   Cost = 290},
                new WarshipPowerLevelModel {PowerPoints = 210,   Cost = 480},
                new WarshipPowerLevelModel {PowerPoints = 340,   Cost = 800},
                new WarshipPowerLevelModel {PowerPoints = 550,   Cost = 1250}
            }
        };
        
        public WarshipPowerScaleModel Create()
        {
            return warshipPowerScaleModel;
        }
    }
}