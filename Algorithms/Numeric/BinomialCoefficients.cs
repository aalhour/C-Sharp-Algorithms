using System;
using System.Collections.Generic;

namespace Algorithms.Numeric
{
    public static class BinomialCoefficients
    {
        /// <summary>
        /// Calculate binomial coefficients, C(n, k).
        /// </summary>
        public static ulong Calculate(uint n, uint k)
        {
            ulong result = 1;

            // Since C(n, k) = C(n, n-k)
            if (k > n - k)
                k = n - k;

            // Calculate value of [n*(n-1)*---*(n-k+1)] / [k*(k-1)*---*1]
            for (int i = 0; i < k; ++i)
            {
                result *= Convert.ToUInt64(n - i);
                result /= Convert.ToUInt64(i + 1);
            }

            return result;
        }

    }

}
