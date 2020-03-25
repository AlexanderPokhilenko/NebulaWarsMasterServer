﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer.BattleStatus
{
    [ZeroFormattable]
    public struct PlayerTrackingMessage:ITypedMessage
    {
        [Index(0)] public readonly int PlayerId;
        
        public PlayerTrackingMessage(int playerId)
        {
            PlayerId = playerId;
        }
        
        public MessageType GetMessageType()
        {
            return MessageType.PlayerTracking;
        }
    }
}