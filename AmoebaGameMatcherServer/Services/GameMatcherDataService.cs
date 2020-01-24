using System;
using System.Collections.Concurrent;
using AmoebaGameMatcherServer.Experimental;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherDataService
    {
        public int Id;
        // номер комнаты + участники комнаты
        public readonly ConcurrentDictionary<int, GameRoomData> GameRoomsData;
        // id игрока + номер его комнаты
        public readonly ConcurrentDictionary<string, int> PlayersInGameRooms;
        // Несортированные игроки могут находиться в этой коллекции не дольше Globals.maxStandbyTimeSec секунд.
        public readonly ConcurrentQueue<PlayerRequest> UnsortedPlayers;

        public GameMatcherDataService()
        {
            Id = new Random().Next(100,1_000);
            GameRoomsData = new ConcurrentDictionary<int, GameRoomData>();
            PlayersInGameRooms = new ConcurrentDictionary<string, int>();
            UnsortedPlayers = new ConcurrentQueue<PlayerRequest>();
        }
    }
}