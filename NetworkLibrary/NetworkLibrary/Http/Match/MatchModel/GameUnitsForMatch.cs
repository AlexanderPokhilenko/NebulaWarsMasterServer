﻿﻿﻿﻿using System;
 using System.Collections;
 using System.Collections.Generic;
  using System.Linq;
  using ZeroFormatter;
  
namespace NetworkLibrary.NetworkLibrary.Http
{
    //TODO Это костыльное говнище
    [ZeroFormattable]
    public class GameUnitsForMatch : IEnumerable<GameUnit>
    {
        [Index(0)] public virtual List<PlayerInfoForMatch> Players { get; set; }
        [Index(1)] public virtual List<BotInfo> Bots { get; set; }

        [IgnoreFormat] 
        public GameUnit this[int index]
        {
            get
            {
                if (index < Players?.Count)
                {
                    return Players[index];
                }
                else if (index < Bots?.Count + Players?.Count)
                {
                    return Bots[index - Players.Count];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }
        
        public int Count()
        {
            int sum = 0;
            if (Players != null)
            {
                sum += Players.Count;
            }

            if (Bots != null)
            {
                sum += Bots.Count;
            }
            
            return sum;
        }

        public IEnumerator<GameUnit> GetEnumerator()
        {
            List<GameUnit> gameUnits = new List<GameUnit>();
            var playersGameUnits = Players?.Select(player => (GameUnit) player);
            var botsGameUnits = Bots?.Select(bot => (GameUnit) bot);
            if (playersGameUnits != null)
            {
                gameUnits.AddRange(playersGameUnits);
            }
            if (botsGameUnits != null)
            {
                gameUnits.AddRange(botsGameUnits);
            }
            return new GameUnitsEnum(gameUnits);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}