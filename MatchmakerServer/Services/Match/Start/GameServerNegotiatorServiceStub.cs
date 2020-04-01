using System;
using System.Threading.Tasks;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class GameServerNegotiatorServiceStub : IGameServerNegotiatorService
    {
#pragma warning disable 1998
        public async Task SendRoomDataToGameServerAsync(BattleRoyaleMatchData data)
#pragma warning restore 1998
        {
            Console.WriteLine(nameof(SendRoomDataToGameServerAsync));
        }
    }
}