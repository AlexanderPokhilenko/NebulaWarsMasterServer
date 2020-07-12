﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class BotModel
    {
        [Index(0)] public virtual ushort TemporaryId { get; set; }
        [Index(1)] public virtual string BotName { get; set; }
        [Index(2)] public virtual string WarshipName { get; set; }
        [Index(3)] public virtual int WarshipPowerLevel { get; set; }
        
    }
}