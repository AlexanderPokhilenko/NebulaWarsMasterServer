﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class BotInfo:GameUnit
    {
        [Index(4)] public virtual string BotName { get; set; }
    }
}