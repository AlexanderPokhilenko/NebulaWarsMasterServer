﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    [ZeroFormattable]
    public struct BattleRoyalePlayerModel
    {
        [Index(0)] public readonly int AccountId;
        [Index(1)] public readonly string Nickname;
        [Index(2)] public readonly string WarshipName;
        [Index(3)] public readonly int WarshipPowerLevel;
        
        public BattleRoyalePlayerModel(int accountId, string nickname, string warshipName, int warshipPowerLevel)
        {
            Nickname = nickname;
            AccountId = accountId;
            WarshipName = warshipName;
            WarshipPowerLevel = warshipPowerLevel;
        }

        public bool IsBot()
        {
            return AccountId < 0;
        }
    }
}