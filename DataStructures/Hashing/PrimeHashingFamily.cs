/***
 * Prime-Hashing Functions Family.
 * 
 * Implements a simple family of hash functions using primes. 
 * The functions are initialized by randomly selecting primes. 
 * Supports re-generation of functions.
 */

using System;
using DataStructures.Common;

namespace DataStructures.Hashing
{
    /// <summary>
    /// Implements a family of randomized Hash Functions
    /// </summary>
    public class PrimeHashingFamily
    {
        private Random _randomizer { get; set; }
        private int _numberOfHashFunctions { get; set; }
        private int[] _multipliersVector { get; set; }
        private static readonly PrimesList _primes = PrimesList.Instance;

        /// <summary>
        /// Initializes the family with a specified number of hash functions.
        /// </summary>
        public PrimeHashingFamily(int numberOfHashFunctions)
        {
            if (numberOfHashFunctions <= 0)
                throw new ArgumentOutOfRangeException("Number of hash functions should be greater than zero.");

            _randomizer = new Random();
            _numberOfHashFunctions = numberOfHashFunctions;
            _multipliersVector = new int[_numberOfHashFunctions];

            GenerateNewFunctions();
        }

        /// <summary>
        /// Returns number of member hash functions.
        /// </summary>
        public int NumberOfFunctions
        {
            get { return _numberOfHashFunctions; }
        }

        /// <summary>
        /// Generates new hash functions with new randomized multipliers.
        /// </summary>
        public void GenerateNewFunctions()
        {
            // Clear the multipliers vectors
            Array.Clear(_multipliersVector, 0, _multipliersVector.Length);

            for (int i = 0; i < _numberOfHashFunctions; i++)
            {
                var randomIndex = _randomizer.Next(0, _primes.Count - 1);
                _multipliersVector[i] = _primes[randomIndex];
            }
        }

        /// <summary>
        /// Returns hash value of an integer prehash key, given the specified number of the hash function to use.
        /// </summary>
        /// <param name="preHashedKey">Int pre-hash code of an object.</param>
        /// <param name="whichHashFunction">Non-zero, non-negative integer that specified the number of the hash function to use.</param>
        /// <returns></returns>
        public int Hash(int preHashedKey, int whichHashFunction)
        {
            if (whichHashFunction <= 0 || whichHashFunction > _numberOfHashFunctions)
                throw new ArgumentOutOfRangeException("WhichHashFunction parameter should be greater than zero or equal to the number of Hash Functions.");

            int preHashValue = 0;
            int multiplier = _multipliersVector[whichHashFunction - 1];
            var characters = preHashedKey.ToString().ToCharArray();

            return (multiplier * preHashValue);
        }

        /// <summary>
        /// Returns hash value of a string, given the specified number of the hash function to use.
        /// </summary>
        /// <param name="key">string key.</param>
        /// <param name="whichHashFunction">Non-zero, non-negative integer that specified the number of the hash function to use.</param>
        /// <returns></returns>
        public int Hash(string key, int whichHashFunction)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key is either an empty string or null.");

            int preHashValue = 0;
            var characters = key.ToCharArray();

            foreach (var character in characters)
            {
                preHashValue += Convert.ToInt32(Char.GetNumericValue(character));
            }

            return Hash(preHashValue, whichHashFunction);
        }

    }

}
