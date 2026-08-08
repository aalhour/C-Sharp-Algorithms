using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.Linq;

/***
* Generates all prime numbers up to a given number
* Wikipedia: http://e-maxx.ru/algo/prime_sieve_linear
*/


namespace Algorithms.Numeric
{
    public static class LinearSieveOfEratosthenes
    {
        /// <summary>
        /// Calculate primes up to a given number
        /// Time Complexity: O(N)
        /// Memory: O(N)
        /// </summary>
        public static List<int> GeneratePrimesUpTo(int N)
        {
            //if N is negative, we should return empty list of primes
            if (N < 0) return new List<int>();
             

            int[] lp=new int[N+1];

            //List of primes
            var pr = new List<int>();

            int op = 0;
            for (int i = 2; i <= N; ++i)
            {
                if (lp[i] == 0)
                {
                    lp[i] = i;
                    pr.Add(i);
                }
                for (int j = 0; j < pr.Count && pr[j] <= lp[i] && i * pr[j] <= N; ++j)
                {
                    lp[i * pr[j]] = pr[j];
                    op++;
                }
            } 
            
            return pr;
        }

    }
}
