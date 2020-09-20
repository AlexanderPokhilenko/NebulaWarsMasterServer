﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;

namespace NetworkLibrary.Http.Utils
{
    public static class CaesarExtension
    {
        public static string Caesar(this string source)
        {
            return source.Caesar(10);
        }

        public static string Caesar(this string source, Int16 shift)
        {
            int maxChar = Convert.ToInt32(char.MaxValue);
            int minChar = Convert.ToInt32(char.MinValue);
            char[] buffer = source.ToCharArray();
            for (var i = 0; i < buffer.Length; i++)
            {
                int shifted = Convert.ToInt32(buffer[i]) + shift;
                if (shifted > maxChar)
                {
                    shifted -= maxChar;
                }
                else if (shifted < minChar)
                {
                    shifted += maxChar;
                }

                buffer[i] = Convert.ToChar(shifted);
            }

            return new string(buffer);
        }
    }
}