using System;
using DataLayer.Tables;
using Google.Apis.Upload;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    //TODO говно
    /// <summary>
    /// Нужен для хранения запроса в очереди в бой.
    /// </summary>
    public class QueueInfoForPlayer
    {
        public readonly DateTime DictionaryEntryTime;
        private readonly PlayerInfoForMatch playerInfoForMatch;
        private readonly int warshipId;

        public QueueInfoForPlayer(string playerServiceId, int accountId, string warshipPrefabName,
            int warshipCombatPowerLevel, int warshipId,  DateTime dictionaryEntryTime)
        {
            playerInfoForMatch = new PlayerInfoForMatch();
            playerInfoForMatch.ServiceId = playerServiceId;
            playerInfoForMatch.AccountId = accountId;
            playerInfoForMatch.TemporaryId = accountId;
            playerInfoForMatch.PrefabName = warshipPrefabName;
            playerInfoForMatch.WarshipCombatPowerLevel = warshipCombatPowerLevel;
            this.warshipId = warshipId;
            DictionaryEntryTime = dictionaryEntryTime;
        }

        public PlayerInfoForMatch GetPlayer()
        {
            return playerInfoForMatch;
        }

        public int GetAccountId()
        {
            return playerInfoForMatch.AccountId;
        }

        public int GetWarshipId()
        {
            return warshipId;
        }

        public string GetPlayerServiceId()
        {
            return string.Copy(playerInfoForMatch.ServiceId);
        }
    }
}