﻿﻿﻿﻿using ZeroFormatter;

   namespace NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.Ping
{
    [ZeroFormattable]
    public struct PlayerPingMessage
    {
        [Index(0)] public string PlayerGoogleId;
        [Index(1)] public int GameRoomNumber;
        public PlayerPingMessage(string playerGoogleId, int gameRoomNumber)
        {
            PlayerGoogleId = playerGoogleId;
            GameRoomNumber = gameRoomNumber;
        }
    }
}