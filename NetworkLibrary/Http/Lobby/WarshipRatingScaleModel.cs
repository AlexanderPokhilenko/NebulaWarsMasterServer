﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Информация о шкале рейтинга для кораблей
    /// </summary>
    [ZeroFormattable]
    public class WarshipRatingScaleModel
    {
        /// <summary>
        /// Максимальный рейтинг (кол-во кубков) после которого будет новый ранг.
        /// </summary>
        [Index(0)] public virtual int[] RankMaxRatingArray { get; set; }
    }
}