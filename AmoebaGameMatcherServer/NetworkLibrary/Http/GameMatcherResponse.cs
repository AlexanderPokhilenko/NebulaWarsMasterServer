﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class GameMatcherResponse
    {
        [Index(0)] public virtual bool PlayerHasJustBeenRegistered{ get; set; }
        [Index(1)] public virtual bool PlayerInQueue{ get; set; }
        [Index(2)] public virtual bool PlayerInBattle{ get; set; }
        [Index(3)] public virtual GameRoomData GameRoomData{ get; set; }
        [Index(4)] public virtual int NumberOfPlayersInQueue{ get; set; }
        [Index(5)] public virtual int NumberOfPlayersInBattles{ get; set; }
    }
}