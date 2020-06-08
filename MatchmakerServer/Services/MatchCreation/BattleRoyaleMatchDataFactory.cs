using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    /// <summary>
    /// Упаковывает данные про только что созданный матч (батл рояль режим) в объект для отправки на гейм сервер.
    /// </summary>
    public static class BattleRoyaleMatchDataFactory
    {
        public static BattleRoyaleMatchModel Create(GameUnitsForMatch gameUnitsForMatch, Match match)
        {
            var result = new BattleRoyaleMatchModel
            {
                MatchId = match.Id,
                GameServerIp = match.GameServerIp,
                GameServerPort = match.GameServerUdpPort,
                GameUnitsForMatch = new GameUnitsForMatch
                {
                    Bots = gameUnitsForMatch.Bots,
                    Players = gameUnitsForMatch.Players
                }
            };
            return result;
        }
    }
}