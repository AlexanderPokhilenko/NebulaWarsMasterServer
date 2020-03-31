﻿using ZeroFormatter;

namespace Libraries.NetworkLibrary.Experimental
{
    [ZeroFormattable]
    public class PlayerAchievements
    {
        [Index(0)] public virtual string SpaceshipPrefabName { get; set; }
        [Index(1)] public virtual int OldSpaceshipRating { get; set; }
        [Index(2)] public virtual int BattleRatingDelta { get; set; }
        [Index(3)] public virtual int RankingRewardTokens { get; set; }
        [Index(4)] public virtual bool DoubleTokens { get; set; }
    }
}