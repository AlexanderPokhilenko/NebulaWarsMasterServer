﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
 using ZeroFormatter;

namespace Libraries.NetworkLibrary.Experimental
{
    [ZeroFormattable]
    public class MatchResultDto
    {
        [Index(0)] public virtual string SkinName { get; set; }
        [Index(1)] public virtual int CurrentWarshipRating { get; set; }
        [Index(2)] public virtual int MatchRatingDelta { get; set; }
        /// <summary>
        /// В словаре хранятся причины/задачи/модификаторы за которые даётся награда. 
        /// </summary>
        [Index(3)] public virtual Dictionary<MatchRewardTypeEnum, int> LootboxPoints { get; set; } 
    }
}