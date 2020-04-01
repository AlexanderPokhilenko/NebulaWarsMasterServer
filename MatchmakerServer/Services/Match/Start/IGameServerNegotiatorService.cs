using System.Threading.Tasks;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public interface IGameServerNegotiatorService
    {
        Task SendRoomDataToGameServerAsync(BattleRoyaleMatchData data);
    }
}