using System;
using System.Collections.Generic;

namespace AmoebaGameMatcherServer.Controllers
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list, int randomSeed)  
        {  
            Random random = new Random(randomSeed);  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = random.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
    }
}