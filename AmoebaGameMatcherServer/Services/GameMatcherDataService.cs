using System;
using System.Collections.Concurrent;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class GameMatcherDataService
    {
        // номер комнаты + участники комнаты
        public readonly ConcurrentDictionary<int, GameRoomData> GameRoomsData;
        // id игрока + номер его комнаты
        public readonly ConcurrentDictionary<string, int> PlayersInGameRooms;
        // id игрока + время добавления в словарь
        public readonly ConcurrentDictionary<string, DateTime> UnsortedPlayers;

        public GameMatcherDataService()
        {
            GameRoomsData = new ConcurrentDictionary<int, GameRoomData>();
            PlayersInGameRooms = new ConcurrentDictionary<string, int>();
            UnsortedPlayers = new ConcurrentDictionary<string, DateTime>();
        }
    }
}