using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages
{
    [ZeroFormattable]
    public struct Transform
    {
        [Index(0)] public float x;
        [Index(1)] public float y;
        [Index(2)] public float angle;

        public Transform(float x, float y, float angle)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
        }

        public Transform(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.angle = 0f;
        }

        public Transform(Vector2 position)
        {
            this.x = position.X;
            this.y = position.Y;
            this.angle = 0f;
        }

        public Vector2 GetPosition() => new Vector2(x, y);
    }
}