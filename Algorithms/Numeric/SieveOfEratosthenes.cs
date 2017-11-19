using System;
using System.Collections.Generic;

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
        public static List<int> GeneratePrimesUpTo(int x)
        {

            //The list of primes that will be returned
            List<int> primesList = new List<int>();

            //Returns an empty list if x is a value under 2
            if (x < 2)
            {
                return primesList;
            }

            //Adds every number between 2 and x to the list
            for (int i = 2; i <= x; i++)
            {
                primesList.Add(i);
            }

            //integer that all multiples of will be removed from the list
            int removeMultiplesOf;

            //Finds the next number in the list that hasn't been removed and removes all multiples of that number 
            //from the list
            for (int i = 2; i <= Math.Sqrt(x); i++)
            {
                if (primesList.Contains(i))
                {
                    removeMultiplesOf = i;
                    for (int j = removeMultiplesOf*removeMultiplesOf; j <= x; j += removeMultiplesOf)
                    {
                        primesList.Remove(j);
                    }
                }
            }

            //The list of primes is returned
            return primesList;
        }

    }
}
