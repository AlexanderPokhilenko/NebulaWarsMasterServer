using System.Threading.Tasks;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    /// <summary>
    /// Читает всю информацию про корабли аккаунта.
    /// </summary>
    public interface IDbAccountWarshipReaderService
    {
        [ItemCanBeNull]
        Task<AccountDbDto> ReadAsync(string playerServiceId);
    }
}