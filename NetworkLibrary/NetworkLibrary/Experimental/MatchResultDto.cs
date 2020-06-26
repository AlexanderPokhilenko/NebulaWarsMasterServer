﻿using System.Collections.Generic;
 using NetworkLibrary.NetworkLibrary.Http;
 using ZeroFormatter;

namespace Libraries.NetworkLibrary.Experimental
{
    [ZeroFormattable]
    public class MatchResultDto
    {
        [Index(0)] public virtual string WarshipPrefabName { get; set; }
        [Index(1)] public virtual int CurrentWarshipRating { get; set; }
        [Index(2)] public virtual int MatchRatingDelta { get; set; }
        /// <summary>
        /// MatchRewardName + кол-во очков для лутбокса
        /// </summary>
        [Index(3)] public virtual Dictionary<MatchRewardTypeEnum, int> LootboxPoints { get; set; } 
    }
}