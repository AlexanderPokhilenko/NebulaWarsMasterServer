﻿﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.ServerToPlayer
{
    [ZeroFormattable]
    public struct HealthPointsMessage:ITypedMessage
    {
        [Index(0)] public readonly float Value;
        
        public HealthPointsMessage(float value)
        {
            Value = value;
        }

        public MessageType GetMessageType()
        {
            return MessageType.HealthPoints;
        }
    }
}