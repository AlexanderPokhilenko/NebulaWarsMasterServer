﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;

namespace NetworkLibrary.NetworkLibrary.Http
{
    /// <summary>
    /// Информация о шкале силы для кораблей. 
    /// </summary>
    public static class WarshipPowerScale
    {
        private const int ExpectedCapacity = 10;
        private static int currentCount = 3;
        private static readonly List<int> Fibonacci = new List<int>(ExpectedCapacity) { 2, 3, 5 };
        private static readonly List<int> TripleSum = new List<int>(ExpectedCapacity) { 4, 7, 15 };
        private const int powerPointsCoefficient = 10;
        private const int softCurrencyCoefficient = 5;

        static WarshipPowerScale()
        {
            Fill(ExpectedCapacity);
        }

        public static WarshipImprovementModel GetModel(int powerLevel)
        {
            if (powerLevel > currentCount)
            {
                Fill(powerLevel);
            }

            powerLevel--; // Мы считаем с 1, а не с 0

            return new WarshipImprovementModel
            {
                PowerPointsCost = Fibonacci[powerLevel] * powerPointsCoefficient,
                SoftCurrencyCost = TripleSum[powerLevel] * softCurrencyCoefficient
            };
        }

        private static void Fill(int index)
        {
            while (currentCount < index)
            {
                Fibonacci.Add(Fibonacci[currentCount - 1] + Fibonacci[currentCount - 2]);
                TripleSum.Add(TripleSum[currentCount - 1] + TripleSum[currentCount - 2] + TripleSum[currentCount - 3]);
                currentCount++;
            }
        }
    }
}