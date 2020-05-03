using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Controllers
{
    /// <summary>
    /// Отвечает за хранение информации о шкале рейтинга для кораблей.
    /// </summary>
    public class WarshipRatingScaleService
    {
        /// <summary>
        /// Значение в миллион не должно быть использовано. Иначе система наград за бой работает не правильно
        /// </summary>
        private readonly WarshipRatingScaleModel warshipRatingScaleModel = new WarshipRatingScaleModel
        {
            RankMaxRatingArray = new []{10, 15, 25, 40, 60, 90, 135, 200, 300, 450, 675, 1010, 1515, 2270, 3400, 1_000_000}
        };
        
        public WarshipRatingScaleModel GetWarshipRatingScaleModel()
        {
            return warshipRatingScaleModel;
        }
    }
}