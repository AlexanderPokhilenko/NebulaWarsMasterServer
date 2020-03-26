using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    public class MyQueue
    {
        //key is playerServiceId
        private readonly ConcurrentDictionary<string, PlayerQueueInfo> unsortedPlayers;

        public MyQueue()
        {
            unsortedPlayers = new ConcurrentDictionary<string, PlayerQueueInfo>();
        }
        
        public bool TryEnqueuePlayer(string playerServiceId, Warship warship)
        {
            var playerInfo = new PlayerQueueInfo
            {
                PlayerServiceId = playerServiceId,
                DictionaryEntryTime = DateTime.UtcNow,
                Warship = warship
            };

            return unsortedPlayers.TryAdd(playerServiceId, playerInfo);
        }

        public bool TryRemove(string playerServiceId)
        {
            return unsortedPlayers.TryRemove(playerServiceId, out var value);
        }

        public bool ContainsPlayer(string playerServiceId)
        {
            return unsortedPlayers.ContainsKey(playerServiceId);
        }

        public int GetCountOfPlayers()
        {
            return unsortedPlayers.Count;
        }

        public DateTime? GetOldestRequestTime()
        {
            if (unsortedPlayers.Count == 0)
            {
                return null;
            }
            DateTime oldestRequestTime = unsortedPlayers
                .Select(pair => pair.Value.DictionaryEntryTime)
                .Min(dateTime => dateTime);
            return oldestRequestTime;
        }

        public List<PlayerQueueInfo> TakeHead(int maxNumberOfPlayersInBattle)
        {
            var playersInfo = unsortedPlayers.Values
                .OrderBy(info => info.DictionaryEntryTime)
                .Take(maxNumberOfPlayersInBattle)
                .ToList();
            return playersInfo;
        }
    }
}