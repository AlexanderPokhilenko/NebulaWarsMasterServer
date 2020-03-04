﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class GameRoomData
    {
        [Index(0)] public virtual string GameServerIp{ get; set; }
        [Index(1)] public virtual int GameRoomNumber{ get; set; }
        [Index(2)] public virtual PlayerInfoForGameRoom[] Players { get; set; }
        [Index(3)] public virtual int GameServerPort{ get; set; }
    }
}