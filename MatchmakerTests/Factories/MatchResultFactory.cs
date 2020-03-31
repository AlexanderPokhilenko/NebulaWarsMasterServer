using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Tables;
using JetBrains.Annotations;

namespace MatchmakerTest.Utils
{
    public static class MatchResultFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="account">Полностью заполненный аккаунт с корабём. Нужно доставать из БД.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<MatchResultForPlayer> Create([NotNull] Account account)
        {
            if (account.Warships.Count == 0)
            {
                throw new ArgumentException(nameof(account.Warships.Count));
            }

            if (account.Id == 0)
            {
                throw new ArgumentException(nameof(account.Id));
            }

            List<MatchResultForPlayer> result = new List<MatchResultForPlayer>()
            {
                new MatchResultForPlayer()
                {
                    Account = account,
                    Warship = account.Warships.First()
                }
            };
            return result;
        }
    }
}