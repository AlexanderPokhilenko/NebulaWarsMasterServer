﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class LobbyModel
    {
        [Index(0)] public virtual AccountDto AccountDto { get; set; }
        [Index(1)] public virtual RewardsThatHaveNotBeenShown RewardsThatHaveNotBeenShown { get; set; }
        [Index(2)] public virtual WarshipRatingScaleModel WarshipRatingScaleModel { get; set; }
    }
}