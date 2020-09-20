﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Http
{
    public class GameUnitModel
    {
       public readonly int AccountId;
       public readonly string Nickname;
       public readonly string SkinName;
       public readonly string WarshipName;
       public readonly ushort TemporaryId;
       public readonly int WarshipPowerLevel;

       public GameUnitModel(PlayerModel playerModel)
       {
           AccountId = playerModel.AccountId;
           Nickname = playerModel.Nickname;
           WarshipName = playerModel.WarshipName;
           TemporaryId = playerModel.TemporaryId;
           WarshipPowerLevel = playerModel.WarshipPowerLevel;
           SkinName = playerModel.SkinName;
       }
       
       public GameUnitModel(BotModel botModel)
       {
           AccountId = -botModel.TemporaryId;
           Nickname = botModel.BotName;
           WarshipName = botModel.WarshipName;
           TemporaryId = botModel.TemporaryId;
           WarshipPowerLevel = botModel.WarshipPowerLevel;
           SkinName = botModel.WarshipName;
       }

       public bool IsBot()
       {
           return AccountId < 0;
       }
    }
    /// <summary>
    /// Модель игрока для клиента
    /// </summary>
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