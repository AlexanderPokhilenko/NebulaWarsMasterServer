﻿﻿using NetworkLibrary.NetworkLibrary.Udp;
using ZeroFormatter;

namespace Libraries.NetworkLibrary.Udp.PlayerToServer
{
    [ZeroFormattable]
    public struct BattleExitMessage:ITypedMessage
    {
        [Index(0)] public int PlayerId;

        public BattleExitMessage(int playerId)
        {
            PlayerId = playerId;
        }

        public MessageType GetMessageType() => MessageType.PlayerExit;
    }
}