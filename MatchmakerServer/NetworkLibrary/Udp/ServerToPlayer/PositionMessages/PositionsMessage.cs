﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages
{
    [ZeroFormattable]
    public class PositionsMessage : ITypedMessage
    {
        [Index(0)] public virtual Dictionary<int, ViewTransform> EntitiesInfo { get; set; }
        //TODO: перенести в UDP с подтверждением
        [Index(1)] public virtual int PlayerEntityId { get; set; }

        [Index(2)] public virtual Dictionary<int, float> RadiusInfo { get; set; }

        public PositionsMessage()
        {
            //EntitiesInfo = new Dictionary<int, ViewTransform>();
        }

        public MessageType GetMessageType() => MessageType.Positions;
    }

    [ZeroFormattable]
    public struct Vector2
    {
        [Index(0)] public float X;
        [Index(1)] public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}; {Y})";
        }

#if UNITY_5_3_OR_NEWER
        public static implicit operator UnityEngine.Vector2(Vector2 vector) => new UnityEngine.Vector2(vector.X, vector.Y);
        public static implicit operator Vector2(UnityEngine.Vector2 vector) => new Vector2(vector.x, vector.y);
#endif
    }
}