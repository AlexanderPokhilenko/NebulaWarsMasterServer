using System;
using System.Collections.Generic;
using System.Linq;
using AmoebaGameMatcherServer.Services;
using AmoebaGameMatcherServer.Services.Queues;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace MatchmakerTest.Utils
{
    public static class PlayersQueueInfoFactory
    {
        public static List<QueueInfoForPlayer> CreateSinglePlayer([NotNull] Account account)
        {
            if (account.Id == 0)
            {
                throw new Exception("Нужен accountId. ");
            }
            List<QueueInfoForPlayer> result = new List<QueueInfoForPlayer>()
            {
                new QueueInfoForPlayer(
                    account.ServiceId,
                    account.Id,
                    "hare",
                    1, account.Warships.First().Id, DateTime.UtcNow)
            };
            return result;
        }
    }
}