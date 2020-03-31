using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Tables;
using Google.Apis.Upload;
using NetworkLibrary.NetworkLibrary.Http;

namespace AmoebaGameMatcherServer.Services
{
    /// <summary>
    /// Отвечает за добавление/удаление игроков в очередь батл рояль режима
    /// </summary>
    public class BattleRoyaleQueueSingletonService
    {
        private readonly MyQueue unsortedPlayers = new MyQueue();
        
        /// <summary>
        /// Добавляет данные в очередь без проверки.
        /// </summary>
        public bool TryEnqueuePlayer(string playerServiceId, Warship warship)
        {
            return unsortedPlayers.TryEnqueuePlayer(playerServiceId, warship);
        }

        public bool TryRemovePlayerFromQueue(string playerServiceId)
        {
            Console.WriteLine("Удаление игрока с id = "+playerServiceId + " из очереди.");
            return unsortedPlayers.TryRemove(playerServiceId);
        }
        
        public bool IsPlayerInQueue(string playerId)
        {
            Console.WriteLine($"Обработка запроса от игрока. кол-во в очереди {unsortedPlayers.GetCountOfPlayers()}. ");
            return unsortedPlayers.ContainsPlayer(playerId);
        }
        
        public int GetNumberOfPlayersInQueue()
        {
            return unsortedPlayers.GetCountOfPlayers();
        }

        /// <summary>
        /// Нужно для сервиса запуска боя.
        /// </summary>
        /// <returns>Возвращает время самого запроса на вход в бой, если запросы есть.</returns>
        public DateTime? GetOldestRequestTime()
        {
            return unsortedPlayers.GetOldestRequestTime();
        }

        /// <summary>
        /// Возвращает игроков без исключения из очереди 
        /// </summary>
        /// <param name="maxNumberOfPlayersInBattle"></param>
        public List<QueueInfoForPlayer> GetPlayersQueueInfo(int maxNumberOfPlayersInBattle)
        {
            return unsortedPlayers.TakeHead(maxNumberOfPlayersInBattle);
        }
        
        public void RemovePlayersFromQueue(List<PlayerInfoForMatch> sukaList)
        {
            foreach (var sukaInfo in sukaList)
            {
                TryRemovePlayerFromQueue(sukaInfo.ServiceId);
            }
        }
    }
}