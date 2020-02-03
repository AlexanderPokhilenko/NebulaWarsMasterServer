﻿﻿﻿﻿﻿using ZeroFormatter;

   namespace NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.Ping
{
    [ZeroFormattable]
    public struct PlayerPingMessage
    {
        [Index(0)] public int PlayerTemporaryIdentifierForTheMatch;
        [Index(1)] public int GameRoomNumber;

        public PlayerPingMessage(int playerTemporaryIdentifierForTheMatch, int gameRoomNumber)
        {
            PlayerTemporaryIdentifierForTheMatch = playerTemporaryIdentifierForTheMatch;
            GameRoomNumber = gameRoomNumber;
        }
    }
}