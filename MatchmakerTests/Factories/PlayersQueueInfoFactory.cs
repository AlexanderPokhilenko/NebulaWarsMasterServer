using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace MatchmakerTest.Utils
{
    public static class PlayersQueueInfoFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account">Аккаунт с Id. Нужно зостать из БД.</param>
        /// <returns></returns>
        public static List<QueueInfoForPlayer> CreateSinglePlayer([NotNull] Account account)
        {
            if (account.Id == 0)
            {
                throw new Exception("Нужен accountId. Для этого достань аккаунт из БД.");
            }
            List<QueueInfoForPlayer> result = new List<QueueInfoForPlayer>()
            {
                new QueueInfoForPlayer(
                    account.ServiceId,
                    account.Id,
                    account.Warships.First(),
                    DateTime.UtcNow)
            };
            return result;
        }
    }
}