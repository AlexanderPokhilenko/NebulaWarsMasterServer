using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class BattleRoyaleUnfinishedMatchesSingletonService
    {
        // номер комнаты + участники комнаты
        private readonly ConcurrentDictionary<int, BattleRoyaleMatchData> gameRoomsData;
        // id игрока + номер его комнаты
        private readonly ConcurrentDictionary<string, int> playersInGameRooms;

        public BattleRoyaleUnfinishedMatchesSingletonService()
        {
            gameRoomsData = default;
            playersInGameRooms = default;
        }
        
        public int GetNumberOfPlayersInBattles()
        {
            throw new NotImplementedException();
        }
        
        public bool IsPlayerInMatch(string playerId)
        {
            return playersInGameRooms.ContainsKey(playerId);
        }
        
        public BattleRoyaleMatchData GetMatchData(string playerId)
        {
            if (IsPlayerInMatch(playerId))
            {
                playersInGameRooms.TryGetValue(playerId, out int roomNumber);
                gameRoomsData.TryGetValue(roomNumber, out var roomData);
                return roomData;
            }
            else
            {
                throw new Exception($"Игрок с id={playerId} не находится в словаре игроков в бою");
            }
        }
        
        public bool TryRemovePlayerFromMatch(string playerId)
        {
            if (playersInGameRooms.ContainsKey(playerId))
            {
                if (playersInGameRooms.Remove(playerId, out int roomId))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}