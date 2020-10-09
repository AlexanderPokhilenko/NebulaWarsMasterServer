using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Tables;

namespace AmoebaGameMatcherServer.Services.LobbyInitialization
{
    public interface ISkinsDbReaderService
    {
        Task<Dictionary<int, List<SkinType>>> ReadAsync(int accountId);
    }
}