using System.Threading.Tasks;
using Libraries.NetworkLibrary.Experimental;

namespace AmoebaGameMatcherServer.Services.MatchFinishing
{
    /// <summary>
    /// Достаёт из БД данные о конкретном бое для аккаунта.
    /// </summary>
    public interface IPlayerMatchResultDbReaderService
    {
        Task<MatchResultDto> ReadMatchResultAsync(int matchId, string playerServiceId);
    }
}