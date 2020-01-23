using System.Collections.Concurrent;
using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherDataService
    {
        /// <summary>
        /// номер комнаты + участники комнаты
        /// </summary>
        public readonly ConcurrentDictionary<string, GameRoomData> GameRoomsData;
        /// <summary>
        /// id игрока + номер его комнаты
        /// </summary>
        public readonly ConcurrentDictionary<string, int> PlayersGameRooms;
        /// <summary>
        /// Несортированные игроки могут находиться в этой коллекции не дольше n секунд. Сейчас 20
        /// </summary>
        public readonly ConcurrentQueue<PlayerRequest> UnsortedPlayers;

        public GameMatcherDataService()
        {
            GameRoomsData = new ConcurrentDictionary<string, GameRoomData>();
            UnsortedPlayers = new ConcurrentQueue<PlayerRequest>();
            PlayersGameRooms = new ConcurrentDictionary<string, int>();
        }
    }
}