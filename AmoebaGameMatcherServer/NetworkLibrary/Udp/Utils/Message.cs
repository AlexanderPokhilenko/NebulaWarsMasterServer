﻿﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    [ZeroFormattable]
    public struct Message
    {
        [Index(0)] public int MessageType;
        [Index(1)] public byte[] SerializedMessage;
        [Index(2)] public int MessageId;
        [Index(3)] public bool NeedResponse;
        
        public Message(int messageType, byte[] serializedMessage, int messageId, bool needResponse)
        {
            MessageType = messageType;
            SerializedMessage = serializedMessage;
            MessageId = messageId;
            NeedResponse = needResponse;
        }
    }
}