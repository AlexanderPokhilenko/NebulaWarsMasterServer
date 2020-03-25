﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer.BattleStatus
{
    [ZeroFormattable]
    public struct KillMessage : ITypedMessage
    {
        [Index(0)] public readonly int KillerId;
        [Index(1)] public readonly ViewTypeId KillerType;
        [Index(2)] public readonly int VictimId;
        [Index(3)] public readonly ViewTypeId VictimType;

        public KillMessage(int killerId, ViewTypeId killerType, int victimId, ViewTypeId victimType)
        {
            KillerId = killerId;
            KillerType = killerType;
            VictimId = victimId;
            VictimType = victimType;
        }
        
        public MessageType GetMessageType()
        {
            return MessageType.Kill;
        }
    }
}