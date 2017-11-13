/***
 * Universal-Hashing Functions Family.
 * 
 * Implements a family class of simple universal-hashing functions. 
 * Supports re-generation of functions. 
 * It depends on the Common/PrimesList helper class. 
 * 
 * This class is used in the Cuckoo Hash Table implementation.
 */

using System;

using DataStructures.Common;

namespace DataStructures.Hashing
{
    /// <summary>
    /// Implements a family of Universal Hash Functions
    /// </summary>
    public class UniversalHashingFamily
    {
        // A large prime, arbitrarily chosen
        // In decimal = 2,146,435,069;
        private const int BIG_PRIME = 0x7FEFFFFD;

        private Random Randomizer { get; set; }
        private int NumberOfHashFunctions { get; set; }
        private int[] FirstMultipliersVector { get; set; }
        private int[] SecondMultipliersVector { get; set; }
        private static readonly PrimesList Primes = PrimesList.Instance;

        /// <summary>
        /// Initializes the family with a specified number of hash functions.
        /// </summary>
        public UniversalHashingFamily(int numberOfHashFunctions)
        {
            if (numberOfHashFunctions <= 0)
                throw new ArgumentOutOfRangeException("Number of hash functions should be greater than zero.");

            Randomizer = new Random();
            NumberOfHashFunctions = numberOfHashFunctions;
            FirstMultipliersVector = new int[NumberOfHashFunctions];
            SecondMultipliersVector = new int[NumberOfHashFunctions];

            GenerateNewFunctions();
        }

        /// <summary>
        /// Returns number of member hash functions.
        /// </summary>
        public int NumberOfFunctions => NumberOfHashFunctions;

        /// <summary>
        /// Generates new hash functions with new randomized multipliers.
        /// </summary>
        public void GenerateNewFunctions()
        {
            // Clear the multipliers vectors
            Array.Clear(FirstMultipliersVector, 0, FirstMultipliersVector.Length);
            Array.Clear(SecondMultipliersVector, 0, SecondMultipliersVector.Length);

            int randomMin = 0;
            int randomMax = Primes.Count - 1;

            for (int i = 0; i < NumberOfHashFunctions; i++)
            {
                // Get only the primes that are smaller than the biggest-chosen prime.
                int randomIndex = Randomizer.Next(randomMin, randomMax);

                while (Primes[randomIndex] >= BIG_PRIME)
                    randomIndex = Randomizer.Next(randomMin, randomMax);

                FirstMultipliersVector[i] = Primes[randomIndex];

                // make sure the next prime we choose is different than the first one and less than the biggest-prime.
                randomIndex = Randomizer.Next(randomMin, randomMax);

                while (Primes[randomIndex] >= BIG_PRIME || Primes[randomIndex] == FirstMultipliersVector[i])
                    randomIndex = Randomizer.Next(randomMin, randomMax);

                SecondMultipliersVector[i] = Primes[randomIndex];
            }
        }

        /// <summary>
        /// Returns hash value of a string, given the specified number of the hash function to use.
        /// </summary>
        /// <param name="preHashedKey">Int pre-hash code of an object.</param>
        /// <param name="whichHashFunction">Non-zero, non-negative integer that specified the number of the hash function to use.</param>
        /// <returns></returns>
        public int UniversalHash(int preHashedKey, int whichHashFunction)
        {
            if (whichHashFunction <= 0 || whichHashFunction > NumberOfHashFunctions)
                throw new ArgumentOutOfRangeException("WhichHashFunction parameter should be greater than zero or equal to the number of Hash Functions.");

            int a = FirstMultipliersVector[whichHashFunction - 1];
            int b = SecondMultipliersVector[whichHashFunction - 1];

            return ((a * preHashedKey) + b) % BIG_PRIME;
        }

        /// <summary>
        /// Returns hash value of a string, given the specified number of the hash function to use.
        /// </summary>
        /// <param name="key">string key.</param>
        /// <param name="whichHashFunction">Non-zero, non-negative integer that specified the number of the hash function to use.</param>
        public int UniversalHash(string key, int whichHashFunction)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key is either an empty string or null.");

            int prehash = 0;
            var characters = key.ToCharArray();
            int n = characters.Length;

            for (int i = 0; i < n; ++i)
            {
                prehash = prehash + (characters[i] ^ (n - 1));
            }

            return UniversalHash(prehash, whichHashFunction);
        }
    }
}
