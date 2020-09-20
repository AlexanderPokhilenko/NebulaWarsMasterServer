using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Queues
{
    /// <summary>
    /// Хранит данные о текущих боях.
    /// </summary>
    public class BattleRoyaleUnfinishedMatchesSingletonService
    {
        //matchId + участники комнаты
        private readonly ConcurrentDictionary<int, BattleRoyaleMatchModel> matches;
        // serviceId + matchId
        private readonly ConcurrentDictionary<string, int> playersInMatches;

        public BattleRoyaleUnfinishedMatchesSingletonService()
        {
            matches = new ConcurrentDictionary<int, BattleRoyaleMatchModel>();
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
        
        public bool IsPlayerInMatch(string playerServiceId, int matchId)
        {
            if (playersInMatches.TryGetValue(playerServiceId, out int realMatchId))
            {
                if (matchId == realMatchId)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("Этот игрок в бою, но не в этом");
                }
            }
            else
            {
                Console.WriteLine("Этот игрок не в бою");
            }

            return false;
        }
        
        public BattleRoyaleMatchModel GetMatchModel(string playerServiceId)
        {
            if (playersInMatches.TryGetValue(playerServiceId, out int matchId))
            {
                matches.TryGetValue(matchId, out var battleRoyaleMatchData);
                return battleRoyaleMatchData;    
            }
            else
            {
                throw new Exception("Этот игрок не находится в бою");
            }
        }
        
        public bool TryRemovePlayerFromMatch(string serviceId)
        {
            if (playersInMatches.ContainsKey(serviceId))
            {
                Console.WriteLine("Игрок есть в списке игроков в бою");
                if (playersInMatches.Remove(serviceId, out _))
                {
                    Console.WriteLine("Успешное удаление игрока из списка игроков в бою.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Не удалось удалить игрока из списка игроков в бою");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Игрока не в бою");
                return false;
            }
        }
        
        public bool TryRemoveMatch(int matchId)
        {
            bool success=true;
            //получить всех игроков
            var matchModel = matches[matchId];
            
            //убрать их из коллекции игроков
            foreach (var player in matchModel.GameUnits.Players)
            {
                playersInMatches.TryRemove(player.ServiceId, out _);
            }
            
            //убрать комнату
            if(!matches.TryRemove(matchId, out _))
            {
                Console.WriteLine($"{nameof(TryRemoveMatch)} Не удалось удалить комнату {nameof(matchId)} {matchId}.");
                success = false;
            }
            
            return success;
        }
        
        //TODO добавить чеки
        public void AddPlayersToMatch(BattleRoyaleMatchModel matchModel)
        {
            matches.TryAdd(matchModel.MatchId, matchModel);
            foreach (var playerInfoForMatch in matchModel.GameUnits.Players)
            {
                playersInMatches.TryAdd(playerInfoForMatch.ServiceId, matchModel.MatchId);
            }
        }
    }
}