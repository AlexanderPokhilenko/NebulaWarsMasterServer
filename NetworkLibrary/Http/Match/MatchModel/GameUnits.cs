﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using ZeroFormatter;
  
namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class GameUnits 
    {
        [Index(0)] public virtual List<PlayerModel> Players { get; set; }
        [Index(1)] public virtual List<BotModel> Bots { get; set; }

        public int Count()
        {
            return Players.Count + Bots.Count;
        }
    }
}