﻿﻿﻿﻿﻿﻿﻿using ZeroFormatter;

      namespace NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.UserInputMessage
{
    [ZeroFormattable]
    public struct PlayerJoystickInputMessage
    {
        [Index(0)] public string PlayerGoogleId;
        [Index(1)] public int GameRoomNumber;
        [Index(2)] public float X;
        [Index(3)] public float Y;
        public PlayerJoystickInputMessage(string playerGoogleId, int gameRoomNumber, float x, float y)
        {
            PlayerGoogleId = playerGoogleId;
            GameRoomNumber = gameRoomNumber;
            X = x;
            Y = y;
        }
    }
}