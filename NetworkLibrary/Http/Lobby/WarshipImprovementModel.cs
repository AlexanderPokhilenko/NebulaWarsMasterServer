﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class WarshipImprovementModel
    {
        /// <summary>
        /// Кол-во очков силы для возможности купить улучшение.
        /// </summary>
        [Index(0)] public virtual int PowerPointsCost { get; set; }
        
        /// <summary>
        /// Стоимость перехода на новый уровень в обычной валюте.
        /// </summary>
        [Index(1)] public virtual int SoftCurrencyCost { get; set; }
    }
}