/***
 * Computes the Catalan Numbers. A dynamic-programming solution.
 * 
 * Wikipedia: https://en.wikipedia.org/wiki/Catalan_number
 */

using System.Diagnostics;
using System.Collections.Generic;
using System.Numerics;

namespace Algorithms.Numeric
{
    public static class CatalanNumbers
    {
        /// <summary>
        /// Internal cache.
        /// By default contains the first two catalan numbers for the ranks: 0, and 1.
        /// </summary>
        private static readonly Dictionary<uint, BigInteger> _catalanNumbers = new Dictionary<uint, BigInteger> { { 0, 1 }, { 1, 1 } };

        /// <summary>
        /// Helper function.
        /// </summary>
        private static BigInteger _recursiveHelper(uint rank)
        {
            if (_catalanNumbers.ContainsKey(rank))
                return _catalanNumbers[rank];

            BigInteger number = 0;
            var lastRank = rank - 1;

            for (uint i = 0; i <= lastRank; ++i)
            {
                var firstPart = _recursiveHelper(i);
                var secondPart = _recursiveHelper(lastRank - i);

                if (!_catalanNumbers.ContainsKey(i)) _catalanNumbers.Add(i, firstPart);
                if (!_catalanNumbers.ContainsKey(lastRank - i)) _catalanNumbers.Add(lastRank - i, secondPart);

                number = number + (firstPart * secondPart);
            }

            return number;
        }

        /// <summary>
        /// Public API
        /// </summary>
        public static BigInteger GetNumber(uint rank)
        {
            // Assert the cache is not empty.
            Debug.Assert(_catalanNumbers.Count >= 2);

            return _recursiveHelper(rank);
        }

        /// <summary>
        /// Calculate the number using the Binomial Coefficients algorithm
        /// </summary>
        public static BigInteger GetNumberByBinomialCoefficients(uint rank)
        {
            // Calculate by binomial coefficient.
            return BinomialCoefficients.Calculate(rank);
        }

        /// <summary>
        /// Return the list of catalan numbers between two ranks, inclusive.
        /// </summary>
        public static List<BigInteger> GetRange(uint fromRank, uint toRank)
        {
            var numbers = new List<BigInteger>();

            if (fromRank > toRank)
                return null;

            for (var i = fromRank; i <= toRank; ++i)
                numbers.Add(GetNumber(i));

            return numbers;
        }
    }
}
