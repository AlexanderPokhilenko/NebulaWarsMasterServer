﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
 using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    public enum SkinTypeEnum
    {
        Hare=1,
        Bird=2,
        Smiley=3,
        Raven=4
    }
    
    [ZeroFormattable]
    public class SkinTypeDto
    {
        [Index(0)] public virtual SkinTypeEnum Id { get; set; }
        [Index(1)] public virtual string Name { get; set; }
    }
    
    [ZeroFormattable]
    public class WarshipDto
    {
        [Index(0)] public virtual int Id { get; set; }
        [Index(1)] public virtual int Rating { get; set; }
        [Index(2)] public virtual int PowerLevel { get; set; }
        [Index(3)] public virtual int PowerPoints { get; set; }
        [Index(4)] public virtual string Description { get; set; }
        [Index(5)] public virtual string CombatRoleName { get; set; }
        [Index(6)] public virtual string WarshipName { get; set; }
        [Index(7)] public virtual WarshipCharacteristics WarshipCharacteristics { get; set; }
        [Index(8)] public virtual List<SkinTypeDto> Skins { get; set; }
        [Index(9)] public virtual SkinTypeDto CurrentSkinType { get; set; }
    }

    [ZeroFormattable]
    public class WarshipCharacteristics
    {
        [Index(0)] public virtual WarshipParameter[] DefenceParameters { get; set; }
        [Index(1)] public virtual WarshipParameter[] AttackParameters { get; set; }
        [Index(2)] public virtual string AttackName { get; set; }
        [Index(3)] public virtual string AttackDescription { get; set; }
        [Index(4)] public virtual WarshipParameter[] UltimateParameters { get; set; }
        [Index(5)] public virtual string UltimateName { get; set; }
        [Index(6)] public virtual string UltimateDescription { get; set; }
    }

    [ZeroFormattable]
    public class WarshipParameter
    {
        [Index(0)] public virtual string Name { get; set; }
        /// <summary>
        /// Позиция в массиве - уровень.
        /// </summary>
        [Index(1)] public virtual string[] Values { get; set; }
        [Index(2)] public virtual string[] Increments { get; set; }
        [Index(3)] public virtual UiIncrementTypeEnum UiIncrementTypeEnum { get; set; }
    }

    public enum UiIncrementTypeEnum
    {
        None,
        Plus,
        Replace
    }
}