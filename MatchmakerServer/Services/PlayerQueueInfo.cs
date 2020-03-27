using System;
using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Нужен для хранения запроса в очереди в бой.
    /// </summary>
    public class PlayerQueueInfo
    {
        public readonly string PlayerServiceId;
        public readonly int AccountId;
        public readonly DateTime DictionaryEntryTime;
        public readonly Warship Warship;

        public PlayerQueueInfo(string playerServiceId, int accountId, Warship warship,  DateTime dictionaryEntryTime)
        {
            PlayerServiceId = playerServiceId;
            AccountId = accountId;
            Warship = warship;
            DictionaryEntryTime = dictionaryEntryTime;
        }

        public PlayerInfoForMatch ToMatchInfo()
        {
            var result = new PlayerInfoForMatch
            {
                AccountId = AccountId,
                IsBot = false,
                ServiceId = Warship.Account.ServiceId,
                TemporaryId = 0,
                WarshipId = Warship.Id,
                WarshipName = Warship.WarshipType.Name,
                WarshipCombatPowerLevel = Warship.CombatPowerLevel
            };
            return result;
        }
    }
}