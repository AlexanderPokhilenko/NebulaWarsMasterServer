using System;
using System.Collections.Generic;
using System.Linq;

namespace AmoebaGameMatcherServer.Services.MatchCreation
{
    /// <summary>
    /// Создаёт уникальные идентификаторы для игроков на время одного боя.
    /// </summary>
    public static class PlayerTemporaryIdsFactory
    {
        public static List<ushort> Create(int numberOfPlayers)
        {
            Random random = new Random();
            HashSet<ushort> set = new HashSet<ushort>(numberOfPlayers);
            while (set.Count != numberOfPlayers)
            {
                ushort tmpId = (ushort) random.Next(ushort.MaxValue);
                set.Add(tmpId);
            }

            return set.ToList();
        }
    }
}