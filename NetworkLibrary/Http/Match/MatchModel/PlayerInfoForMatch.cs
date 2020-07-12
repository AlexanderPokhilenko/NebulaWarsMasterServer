﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Нужен для гейм сервера. Хранит всю информацию, которая влияет на бой.
    /// </summary>
    [ZeroFormattable]
    public class PlayerModel
    {
        [Index(0)] public virtual ushort TemporaryId { get; set; }
        [Index(1)] public virtual string WarshipName { get; set; }
        [Index(2)] public virtual int WarshipPowerLevel { get; set; }
        [Index(3)] public virtual string ServiceId { get; set; }
        [Index(4)] public virtual int AccountId { get; set; }
        [Index(5)] public virtual string Nickname { get; set; }
        [Index(6)] public virtual string SkinName { get; set; }
    }
}