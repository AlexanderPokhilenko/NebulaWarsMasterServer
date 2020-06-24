using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Queues
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
            int warshipPowerLevel, int warshipId,  DateTime dictionaryEntryTime)
        {
            playerInfoForMatch = new PlayerInfoForMatch
            {
                ServiceId = playerServiceId,
                AccountId = accountId,
                PrefabName = warshipPrefabName,
                WarshipPowerPoints = warshipPowerLevel
            };
            unchecked
            {
                playerInfoForMatch.TemporaryId = (ushort) accountId;
            }

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