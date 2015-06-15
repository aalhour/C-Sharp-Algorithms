using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;
using DataStructures.Hashing;

namespace DataStructures.Hashing
{
    /// <summary>
    /// Implements a family of randomized Hash Functions
    /// </summary>
    public class PrimeHashingFamily
    {
        private Random _randomNumbers { get; set; }
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

            _randomNumbers = new Random();
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
            for (int i = 0; i < _numberOfHashFunctions; i++)
            {
                var randomIndex = _randomNumbers.Next(0, _primes.Count);
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
            if (whichHashFunction <= 0 || whichHashFunction > _multipliersVector.Length)
                throw new ArgumentOutOfRangeException("WhichHashFunction parameter should be greater than zero or equal to the number of Hash Functions.");

            int hashValue = 0;
            int multiplier = _multipliersVector[whichHashFunction - 1];
            var characters = preHashedKey.ToString().ToCharArray();

            foreach (var character in characters)
            {
                hashValue = multiplier * hashValue + Convert.ToInt32(Char.GetNumericValue(character));
            }

            return hashValue;
        }

        /// <summary>
        /// Returns hash value of a string, given the specified number of the hash function to use.
        /// </summary>
        /// <param name="preHashedKey">Int pre-hash code of an object.</param>
        /// <param name="whichHashFunction">Non-zero, non-negative integer that specified the number of the hash function to use.</param>
        /// <returns></returns>
        public int Hash(string key, int whichHashFunction)
        {
            if (whichHashFunction <= 0 || whichHashFunction > _multipliersVector.Length)
                throw new ArgumentOutOfRangeException("WhichHashFunction parameter should be greater than zero or equal to the number of Hash Functions.");

            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("Key is either an empty string or null.");

            int hashValue = 0;
            int multiplier = _multipliersVector[whichHashFunction - 1];
            var characters = key.ToCharArray();

            foreach(var character in characters)
            {
                hashValue = multiplier * hashValue + character;
            }

            return hashValue;
        }

    }

}
