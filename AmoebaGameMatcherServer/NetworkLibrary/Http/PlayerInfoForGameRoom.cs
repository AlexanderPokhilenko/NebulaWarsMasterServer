﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class PlayerInfoForGameRoom
    {
        [Index(0)] public virtual string PlayerGoogleId { get; set; }
        [Index(1)] public virtual bool IsBot { get; set; }
        [Index(2)] public virtual int PlayerTemporaryIdentifierForTheMatch { get; set; }
    }
}