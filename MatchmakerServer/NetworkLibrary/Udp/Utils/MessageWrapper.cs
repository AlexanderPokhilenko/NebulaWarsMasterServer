﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    [ZeroFormattable]
    public struct MessageWrapper
    {
        [Index(0)] public MessageType MessageType;
        [Index(1)] public byte[] SerializedMessage;
        [Index(2)] public uint MessageId;
        [Index(3)] public bool NeedResponse;
        
        public MessageWrapper(MessageType messageType, byte[] serializedMessage, uint messageId, bool needResponse)
        {
            MessageType = messageType;
            SerializedMessage = serializedMessage;
            MessageId = messageId;
            NeedResponse = needResponse;
        }
    }

    public interface ITypedMessage
    {
        MessageType GetMessageType();
    }

    public enum MessageType:sbyte
    {
        PlayerInput = 3,
        PlayerPing = 5,
        Positions = 6,
        DeliveryConfirmation = 7,
        PlayerExit = 8,
        HealthPoints = 9,
        MaxHealthPoints = 11,
        PlayerTracking = 13,
        PointTracking = 14,
        ShowPlayerAchievements = 15,
        Kill = 16
    }
}