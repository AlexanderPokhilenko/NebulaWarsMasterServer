using NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp.PlayerToServer.UserInputMessage
{
    [ZeroFormattable]
    public struct PlayerInputMessage
    {
        [Index(0)] public int TemporaryIdentifier;
        [Index(1)] public int GameRoomNumber;
        [Index(2)] public float X;
        [Index(3)] public float Y;
        [Index(4)] public float Angle;
        public PlayerInputMessage(int identifier, int gameRoomNumber, float x, float y, float angle)
        {
            TemporaryIdentifier = identifier;
            GameRoomNumber = gameRoomNumber;
            X = x;
            Y = y;
            Angle = angle;
        }

        public Vector2 GetVector2() => new Vector2(X, Y);
    }
}