﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class PlayerInfoForGameRoom
    {
        [Index(0)] public virtual string GoogleId { get; set; }
        [Index(1)] public virtual int TemporaryId { get; set; }
    }
}