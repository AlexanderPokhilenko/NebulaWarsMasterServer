using System.Threading.Tasks;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Отвечает за проверку данных игрока перед добавлением в очередь
    /// </summary>
    public interface IQueueExtenderService
    {
        /// <summary>
        /// Проверяет данные и добавляет игрока в очередь.
        /// </summary>
        /// <returns>Вернёт false если в БД нет таких данных или игрок уже в очереди.</returns>
        Task<bool> TryEnqueuePlayerAsync(string playerServiceId, int warshipId);
    }
}