﻿using System.Collections.Generic;
using ZeroFormatter;

namespace NetworkLibrary.NetworkLibrary.Udp.ServerToPlayer.PositionMessages
{
    [ZeroFormattable]
    public class PlayersPositionsMessage
    {
        /// <summary>
        /// playerGoogleId position
        /// </summary>
        [Index(0)] public virtual Dictionary<string, Vector2> PlayersInfo { get; set; }= new Dictionary<string, Vector2>();

        public PlayersPositionsMessage()
        {
        }
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
    }
}