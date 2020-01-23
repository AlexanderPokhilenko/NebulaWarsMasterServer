using System.Collections.Concurrent;
using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherDataService
    {
        /// <summary>
        /// номер комнаты + участники комнаты
        /// </summary>
        public ConcurrentDictionary<int, GameRoomData> gameRoomDatas;
        public readonly ConcurrentQueue<PlayerRequest> unsortedPlayers;

        public GameMatcherDataService()
        {
            gameRoomDatas = new ConcurrentDictionary<int, GameRoomData>();
            unsortedPlayers = new ConcurrentQueue<PlayerRequest>();
        }
    }
}