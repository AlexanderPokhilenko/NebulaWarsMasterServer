﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class LootboxModel
    {
        [Index(0)] public virtual List<ResourceModel> Prizes { get; set; }
    }

    [ZeroFormattable]
    public class ResourceModel
    {
        [Index(0)] public virtual ResourceTypeEnum ResourceTypeEnum { get; set; }
        [Index(1)] public virtual byte[] SerializedModel { get; set; }
    }

    [ZeroFormattable]
    public class SoftCurrencyResourceModel
    {
        [Index(0)] public virtual int Amount { get; set; }
    }
    
    [ZeroFormattable]
    public class HardCurrencyResourceModel
    {
        [Index(0)] public virtual int Amount { get; set; }
    }
    
    [ZeroFormattable]
    public class WarshipPowerPointsResourceModel
    {
        //todo изменить модель
        [Index(0)] public virtual string WarshipSkinName { get; set; }
        [Index(1)] public virtual int StartValue { get; set; }
        [Index(2)] public virtual int FinishValue { get; set; }
        [Index(3)] public virtual int MaxValueForLevel { get; set; }
        [Index(4)] public virtual int? WarshipId { get; set; }
        [Index(5)] public virtual WarshipTypeEnum WarshipTypeEnum { get; set; }

    }

    public enum ResourceTypeEnum
    {
        SoftCurrency,
        WarshipPowerPoints,
        HardCurrency,
        LootboxPoints,
        LootboxSet
    }
}