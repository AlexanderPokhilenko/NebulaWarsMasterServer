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
        public string PlayerServiceId;
        public int AccountId { get; set; }
        public DateTime DictionaryEntryTime;
        public Warship Warship;

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