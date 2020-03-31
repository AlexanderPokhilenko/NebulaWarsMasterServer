using DataLayer.Tables;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Формирует объект с данными про матч на основе записи из БД
    /// </summary>
    public static class MatchDataFactory
    {
        public static BattleRoyaleMatchData Create(GameUnitsForMatch gameUnitsForMatch, Match match)
        {
            var result = new BattleRoyaleMatchData
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