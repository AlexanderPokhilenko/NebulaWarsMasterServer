﻿﻿﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

   namespace NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.Ping
{
    [ZeroFormattable]
    public struct PlayerPingMessage : ITypedMessage
    {
        [Index(0)] public int TemporaryId;
        [Index(1)] public int GameRoomNumber;

        public PlayerPingMessage(int temporaryId, int gameRoomNumber)
        {
            TemporaryId = temporaryId;
            GameRoomNumber = gameRoomNumber;
        }

        public MessageType GetMessageType() => MessageType.PlayerPing;
    }
}