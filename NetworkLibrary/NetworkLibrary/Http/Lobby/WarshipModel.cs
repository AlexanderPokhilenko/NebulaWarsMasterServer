﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class WarshipDto
    {
        //Значение для операций с кораблём
        [Index(0)] public virtual int Id { get; set; }
        //Значение для отображения корабля
        [Index(1)] public virtual ViewTypeId ViewTypeId { get; set; }

        //Значения для рейтинга корабля
        [Index(2)] public virtual int Rating { get; set; }

        //Значения для прокачки корабля (уровень силы)
        [Index(3)] public virtual int PowerLevel { get; set; }
        [Index(4)] public virtual int PowerPoints { get; set; }
        [Index(5)] public virtual string Description { get; set; }
        [Index(6)] public virtual string CombatRoleName { get; set; }
        [Index(7)] public virtual string WarshipName { get; set; }
    }
}