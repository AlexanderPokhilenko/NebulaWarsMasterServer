﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
                       using System.Collections.Generic;
using NetworkLibrary.NetworkLibrary.Http;

namespace NetworkLibrary.Http.Lobby
{
    /// <summary>
    /// Информация о шкале силы для кораблей. 
    /// </summary>
    public static class WarshipPowerScale
    {
        private static int currentCount = 3;
        private const int ExpectedCapacity = 10;
        private const int SoftCurrencyCoefficient = 5;
        private const int PowerPointsCoefficient = 10;
        private static readonly List<int> Fibonacci = new List<int>(ExpectedCapacity) { 2, 3, 5 };
        private static readonly List<int> TripleSum = new List<int>(ExpectedCapacity) { 4, 7, 15 };

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

            //todo это нужно исправить тест не проходит
            powerLevel--; // Мы считаем с 1, а не с 0

            return new WarshipImprovementModel
            {
                PowerPointsCost = Fibonacci[powerLevel] * PowerPointsCoefficient,
                SoftCurrencyCost = TripleSum[powerLevel] * SoftCurrencyCoefficient
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