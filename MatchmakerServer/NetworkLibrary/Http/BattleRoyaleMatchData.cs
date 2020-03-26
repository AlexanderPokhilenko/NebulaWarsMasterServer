﻿﻿﻿﻿using System.Collections.Generic;
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
        [Index(1)] public virtual int MatchId{ get; set; }
        [Index(2)] public virtual List<PlayerInfoForMatch> Players { get; set; }
        [Index(3)] public virtual int GameServerPort{ get; set; }
    }
}