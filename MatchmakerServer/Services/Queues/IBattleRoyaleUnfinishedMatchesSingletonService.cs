using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Queues
{
    /// <summary>
    /// Хранит данные о текущих боях.
    /// </summary>
    public interface IBattleRoyaleUnfinishedMatchesSingletonService
    {
        int GetNumberOfPlayersInBattles();
        bool IsPlayerInMatch(string playerServiceId);
        bool IsPlayerInMatch(string playerServiceId, int matchId);
        BattleRoyaleMatchModel GetMatchModel(string playerServiceId);
        bool TryRemovePlayerFromMatch(string serviceId);
        bool TryRemoveMatch(int matchId);
        void AddPlayersToMatch(BattleRoyaleMatchModel matchModel);
    }
}