using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace AmoebaGameMatcherServer.Services.Queues
{
    public class PlayersQueue
    {
        //key is playerServiceId
        private readonly ConcurrentDictionary<string, MatchEntryRequest> unsortedPlayers
            = new ConcurrentDictionary<string, MatchEntryRequest>();
        
        public bool TryEnqueue(string playerServiceId, MatchEntryRequest playerInfo)
        {
            return unsortedPlayers.TryAdd(playerServiceId, playerInfo);
        }

        public bool TryRemove(string playerServiceId)
        {
            return unsortedPlayers.TryRemove(playerServiceId, out _);
        }

        public bool Contains(string playerServiceId)
        {
            return unsortedPlayers.ContainsKey(playerServiceId);
        }

        public int GetNumberOfPlayers()
        {
            return unsortedPlayers.Count;
        }

        [CanBeNull]
        public DateTime? GetOldestRequestTime()
        {
            if (unsortedPlayers.Count == 0)
            {
                return null;
            }
            
            DateTime oldestRequestTime = unsortedPlayers
                .Select(pair => pair.Value.DictionaryEntryTime)
                .Min();
            return oldestRequestTime;
        }

        public List<MatchEntryRequest> TakeHead(int maxNumberOfPlayersInMatch)
        {
            List<MatchEntryRequest> playersInfo = unsortedPlayers.Values
                .OrderBy(info => info.DictionaryEntryTime)
                .Take(maxNumberOfPlayersInMatch)
                .ToList();
            return playersInfo;
        }
    }
}