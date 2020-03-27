﻿﻿﻿﻿using System;
using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Нужен для передачи данных о бое между матчером и гейм сервером.
    /// </summary>
    [ZeroFormattable]
    public class BattleRoyaleMatchData
    {
        [Index(0)] public virtual string GameServerIp{ get; set; }
        [Index(1)] public virtual int GameServerPort{ get; set; }
        [Index(2)] public virtual int MatchId{ get; set; }
        [Index(3)] public virtual List<PlayerInfoForMatch> Players { get; set; }
        [Index(4)] public virtual List<BotInfo> Bots { get; set; }
    }

    public class GameUnitsForMatch
    {
        public List<PlayerInfoForMatch> Players;
        public List<BotInfo> Bots;

        public int Count()
        {
            int sum = 0;
            if (Players != null)
            {
                sum += Players.Count;
                Console.WriteLine($"Players != null sum = "+sum);
            }

            if (Bots != null)
            {
                sum += Bots.Count;
                Console.WriteLine($"Bots != null sum = "+sum);
            }

            Console.WriteLine($"{nameof(sum)}  = {sum}");
            return sum;
        }
    }
}