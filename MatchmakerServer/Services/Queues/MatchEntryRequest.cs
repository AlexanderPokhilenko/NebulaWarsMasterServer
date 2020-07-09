using System;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Queues
{
    /// <summary>
    /// Отвечает за хранение информацию для входа в бой.
    /// </summary>
    public class MatchEntryRequest
    {
        public readonly DateTime DictionaryEntryTime;
        
        private readonly int warshipId;
        private readonly PlayerModel playerModel;

        public MatchEntryRequest(string playerServiceId, int accountId, string warshipPrefabName,
            int warshipPowerLevel, int warshipId,  DateTime dictionaryEntryTime, string nickname)
        {
            ushort temporaryId;
            unchecked
            {
                temporaryId = (ushort) accountId;
            }
            
            playerModel = new PlayerModel
            {
                WarshipName = warshipPrefabName,
                WarshipPowerLevel = warshipPowerLevel,
                ServiceId = playerServiceId,
                AccountId = accountId,
                TemporaryId = temporaryId,
                Nickname = nickname
            };

            this.warshipId = warshipId;
            DictionaryEntryTime = dictionaryEntryTime;
        }

        public PlayerModel GetPlayerModel()
        {
            return playerModel;
        }

        public int GetWarshipId()
        {
            return warshipId;
        }

        public string GetPlayerServiceId()
        {
            return string.Copy(playerModel.ServiceId);
        }
    }
}