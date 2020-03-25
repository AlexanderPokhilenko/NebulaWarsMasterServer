﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer.BattleStatus
{
    [ZeroFormattable]
    public struct ShowPlayerAchievementsMessage:ITypedMessage
    {
        public MessageType GetMessageType()
        {
            return MessageType.ShowPlayerAchievements;
        }
    }
}