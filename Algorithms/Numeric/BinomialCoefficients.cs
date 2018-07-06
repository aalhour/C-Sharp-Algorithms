using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Numeric
{
    public static class BinomialCoefficients
    {
        private static readonly Dictionary<uint, BigInteger> Cache = new Dictionary<uint, BigInteger>();

        /// <summary>
        /// Calculate binomial coefficients, C(n, k).
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger Calculate(uint n)
        {
            return Factorial(2 * n) / (Factorial(n) * Factorial(n + 1));
        }

        private static BigInteger Factorial(uint n)
        {
            if (n <= 1)
                return 1;
            if (Cache.ContainsKey(n))
            {
                return Cache[n];
            }
            var value = n * Factorial(n - 1);
            Cache[n] = value;
            return value;
        }
    }
}