﻿﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace NetworkLibrary.NetworkLibrary.Http
{
    public class GameUnitsEnum : IEnumerator<GameUnit>
    {
        public List<GameUnit> gameUnits;

        int position = -1;

        public GameUnitsEnum(List<GameUnit> gameUnits)
        {
            this.gameUnits = gameUnits;
        }

        public bool MoveNext()
        {
            position++;
            return position < gameUnits.Count;
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public GameUnit Current
        {
            get
            {
                try
                {
                    return gameUnits[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}