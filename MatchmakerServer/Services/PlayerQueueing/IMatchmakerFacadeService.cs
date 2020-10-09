using System.Threading.Tasks;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.PlayerQueueing
{
    /// <summary>
    /// Отвечает за обработку запросов на вход в бой от клиентов.
    /// </summary>
    public interface IMatchmakerFacadeService
    {
        [ItemNotNull]
        Task<MatchmakerResponse> GetMatchDataAsync([NotNull] string playerServiceId, int warshipId);
    }
}