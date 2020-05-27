using System;

namespace Algorithms.Numeric
{
    public static class GreatestCommonDivisor
    {
        /// <summary>
        ///     Returns the greatest common divisor of two numbers using Euclidean Algorithm.
        /// </summary>
        public static int FindGCDEuclidean(int a, int b)
        {
            int t = 0;

            a = Math.Abs(a);
            b = Math.Abs(b);

            if (a == 0)
                return b;
            if (b == 0)
                return a;

            while (a % b != 0) {
                t = b;
                b = a % b;
                a = t;
            }
            return b;
        }

        /// <summary>
        ///     Returns the greatest common divisor of two numbers using Stein Algorithm.
        /// </summary>
        public static int FindGCDStein(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            return Stein(a, b);
        }

        private static int Stein(int a, int b)
        {
            if (a == 0)
                return b;
            if (b == 0)
                return a;
            if (a == b)
                return a;

            if ((~a & 1) == 1)
            {
                if ((b & 1) == 1)
                {
                    return Stein(a >> 1, b);
                }
                else
                {
                    return Stein(a >> 1, b >> 1) << 1;
                }
            }

            if ((~b & 1) == 1)
            {
                return Stein(a, b >> 1);
            }

            return a > b ? Stein((a - b) >> 1, b) : Stein(a, (b - a) >> 1);
        }
    }
}
