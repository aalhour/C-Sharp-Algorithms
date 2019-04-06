using System;
using System.Collections.Generic;
using System.Linq;

/***
* Generates all prime numbers up to a given number
* Wikipedia: https://en.wikipedia.org/wiki/Sieve_of_Eratosthenes
*/


namespace Algorithms.Numeric
{
    public static class SieveOfEratosthenes
    {
        /// <summary>
        /// Calculate primes up to a given number
        /// </summary>
        public static IEnumerable<int> GeneratePrimesUpTo(int x)
        {

            //The hash of primes that will be returned
            var primes = new HashSet<int>();

            //Returns an empty list if x is a value under 2
            if (x < 2)
            {
                return primes.ToList();
            }

            //Adds every number between 2 and x to the hashset
            for (int i = 2; i <= x; i++)
            {
                primes.Add(i);
            }

            //integer that all multiples of will be removed from the hashset
            int removeMultiplesOf;

            //Finds the next number that hasn't been removed and removes all multiples of that number 
            //from the hashset
            for (int i = 2; i <= Math.Sqrt(x); i++)
            {
                if (primes.Contains(i))
                {
                    removeMultiplesOf = i;
                    for (int j = removeMultiplesOf * removeMultiplesOf; j <= x; j += removeMultiplesOf)
                    {
                        primes.Remove(j);
                    }
                }
            }

            //The list of primes is returned
            return primes.ToList();
        }

    }
}
