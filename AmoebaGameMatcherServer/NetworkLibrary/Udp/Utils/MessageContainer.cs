﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    [ZeroFormattable]
    public struct MessageContainer
    {
        [Index(0)] public Message[] Messages;
        public MessageContainer(Message[] messages)
        {
            Messages = messages;
        }
    }
}