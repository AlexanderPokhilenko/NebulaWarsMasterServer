using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services.Queues
{
    /// <summary>
    /// Отвечает за добавление/удаление игроков в очередь батл рояль режима
    /// </summary>
    public interface IBattleRoyaleQueueSingletonService
    {
        bool TryEnqueue(MatchEntryRequest matchEntryRequest);
        bool TryRemove(string playerServiceId);
        bool Contains(string playerServiceId);
        int GetNumberOfPlayers();
        [CanBeNull]
        DateTime? GetOldestRequestTime();

        /// <summary>
        /// Возвращает игроков без исключения из очереди 
        /// </summary>
        List<MatchEntryRequest> TakeMatchEntryRequests(int maxNumberOfPlayersInBattle);

        void RemovePlayersFromQueue(List<PlayerModel> playerModels);
    }
}