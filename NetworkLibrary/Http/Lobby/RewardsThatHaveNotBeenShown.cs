﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using JetBrains.Annotations;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public class RewardsThatHaveNotBeenShown
    {
        [Index(0)] public virtual int SoftCurrencyDelta {get;set;}
        [Index(1)] public virtual int HardCurrencyDelta {get;set;}
        [Index(2)] public virtual int LootboxPointsDelta {get;set;}
        [Index(3)] public virtual int AccountRatingDelta {get;set;}

        public override string ToString()
        {
            return
                $"{nameof(SoftCurrencyDelta)} {SoftCurrencyDelta} " +
                $"{nameof(LootboxPointsDelta)} {LootboxPointsDelta} " +
                $"{nameof(AccountRatingDelta)} {AccountRatingDelta}";
        }

        public static RewardsThatHaveNotBeenShown operator +([NotNull] RewardsThatHaveNotBeenShown arg1,
            [NotNull] RewardsThatHaveNotBeenShown arg2)
        {
            var shown = new RewardsThatHaveNotBeenShown();
            shown.AccountRatingDelta = arg1.AccountRatingDelta + arg2.AccountRatingDelta;
            shown.HardCurrencyDelta = arg1.HardCurrencyDelta+arg2.HardCurrencyDelta;
            shown.LootboxPointsDelta = arg1.LootboxPointsDelta + arg2.LootboxPointsDelta;
            shown.SoftCurrencyDelta = arg1.SoftCurrencyDelta + arg2.SoftCurrencyDelta;
            return shown;
        }
    }
}