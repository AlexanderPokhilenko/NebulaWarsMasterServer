using System;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages
{
    [ZeroFormattable]
    public struct ViewTransform
    {
        [Index(0)] public float x;
        [Index(1)] public float y;
        [Index(2)] public float angle;
        [Index(3)] public ViewTypeId typeId;

        public ViewTransform(float x, float y, float angle, ViewTypeId typeId)
        {
            this.x = x;
            this.y = y;
            this.angle = angle;
            this.typeId = typeId;
        }

        public ViewTransform(float x, float y, ViewTypeId typeId) : this(x, y, 0f, typeId)
        { }

        public ViewTransform(Vector2 position, ViewTypeId typeId) : this(position.X, position.Y, typeId)
        { }

        public ViewTransform(Vector2 position, float angle, ViewTypeId typeId) : this(position.X, position.Y, angle, typeId)
        { }

        public Vector2 GetPosition() => new Vector2(x, y);

        public static ViewTransform operator +(ViewTransform t1, ViewTransform t2)
        {
            if(t1.typeId != t2.typeId) throw new NotSupportedException(nameof(typeId) + " не совпали!");
            return new ViewTransform(t1.x + t2.x, t1.y + t2.y, t1.angle + t2.angle, t1.typeId);
        }

        public static ViewTransform operator -(ViewTransform t1, ViewTransform t2)
        {
            if (t1.typeId != t2.typeId) throw new NotSupportedException(nameof(typeId) + " не совпали!");
            return new ViewTransform(t1.x - t2.x, t1.y - t2.y, t1.angle - t2.angle, t1.typeId);
        }

        public static ViewTransform operator *(ViewTransform t, float k)
        {
            return new ViewTransform(t.x * k, t.y * k, t.angle * k, t.typeId);
        }

        public static ViewTransform operator *(float k, ViewTransform t) => t * k;

        public static ViewTransform operator /(ViewTransform t, float k)
        {
            return new ViewTransform(t.x / k, t.y / k, t.angle / k, t.typeId);
        }
    }
}