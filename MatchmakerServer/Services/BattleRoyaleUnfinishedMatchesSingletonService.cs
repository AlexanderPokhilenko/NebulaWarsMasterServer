using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
     
    //TODO сделать это говно многопоточным
    /// <summary>
    /// Хранит данные о текущих боях.
    /// </summary>
    public class BattleRoyaleUnfinishedMatchesSingletonService
    {
        // номер матча + участники комнаты
        private readonly ConcurrentDictionary<int, BattleRoyaleMatchData> matchesData;
        // id игрока + номер его матча
        private readonly ConcurrentDictionary<string, int> playersInMatches;
        

        public BattleRoyaleUnfinishedMatchesSingletonService()
        {
            matchesData = new ConcurrentDictionary<int, BattleRoyaleMatchData>();
            playersInMatches = new ConcurrentDictionary<string, int>();
        }
        
        public int GetNumberOfPlayersInBattles()
        {
            return playersInMatches.Count;
        }
        
        public bool IsPlayerInMatch(string playerServiceId)
        {
            return playersInMatches.ContainsKey(playerServiceId);
        }
        
        public BattleRoyaleMatchData GetMatchData(string playerServiceId)
        {
            if (IsPlayerInMatch(playerServiceId))
            {
                playersInMatches.TryGetValue(playerServiceId, out int roomNumber);
                matchesData.TryGetValue(roomNumber, out var roomData);
                return roomData;
            }
            else
            {
                throw new Exception($"Игрок с id={playerServiceId} не находится в словаре игроков в бою");
            }
        }
        
        public bool TryRemovePlayerFromMatch(string playerId)
        {
            if (playersInMatches.ContainsKey(playerId))
            {
                if (playersInMatches.Remove(playerId, out int roomId))
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
        
        //TODO добавить чеки
        public void AddPlayersToMatch(BattleRoyaleMatchData matchData)
        {
            matchesData.TryAdd(matchData.MatchId, matchData);
            foreach (var playerInfoForMatch in matchData.GameUnitsForMatch.Players)
            {
                playersInMatches.TryAdd(playerInfoForMatch.ServiceId, matchData.MatchId);
            }
        }
    }
}