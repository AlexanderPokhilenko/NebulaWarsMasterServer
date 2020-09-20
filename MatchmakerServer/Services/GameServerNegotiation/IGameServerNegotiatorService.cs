using System.Threading.Tasks;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.GameServerNegotiation
{
    public interface IGameServerNegotiatorService
    {
        Task SendRoomDataToGameServerAsync(BattleRoyaleMatchModel model);
    }
}