﻿﻿﻿﻿﻿using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp
{
    [ZeroFormattable]
    public struct MessagesContainer
    {
        [Index(0)] public MessageWrapper[] Messages;
        public MessagesContainer(MessageWrapper[] messages)
        {
            Messages = messages;
        }
    }
}