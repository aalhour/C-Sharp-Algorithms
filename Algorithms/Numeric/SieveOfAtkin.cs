using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

/***
* Generates all prime numbers up to a given number
* Wikipedia: https://en.wikipedia.org/wiki/Sieve_of_Atkin
*/


namespace Algorithms.Numeric
{
    public static class SieveOfAtkin
    {
        /// <summary>
        /// Calculate primes up to a given number
        /// </summary>
        public static List<int> GeneratePrimesUpTo(int max)
        {
            if (max == 2)
            {
                return new List<int>() { 2 };
            }

            if (max < 2)
            {
                return new List<int>();
            }

            var isPrime = new bool[max + 1];
            var sqrt = (int)System.Math.Sqrt(max);

            Parallel.For(1, sqrt, x =>
            {
                var xx = x * x;
                for (var y = 1; y <= sqrt; y++)
                {
                    var yy = y * y;
                    var n = 4 * xx + yy;
                    if (n <= max && (n % 12 == 1 || n % 12 == 5))
                        isPrime[n] ^= true;

                    n = 3 * xx + yy;
                    if (n <= max && n % 12 == 7)
                        isPrime[n] ^= true;

                    n = 3 * xx - yy;
                    if (x > y && n <= max && n % 12 == 11)
                        isPrime[n] ^= true;
                }
            });

            var primes = new List<int>() { 2, 3 };
            for (var n = 5; n <= sqrt; n++)
            {
                if (isPrime[n])
                {
                    primes.Add(n);
                    var nn = n * n;
                    for (var k = nn; k <= max; k += nn)
                        isPrime[k] = false;
                }
            }

            for (var n = sqrt + 1; n <= max; n++)
                if (isPrime[n])
                    primes.Add(n);

            return primes;
        }

    }
}
