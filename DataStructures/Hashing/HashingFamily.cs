using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Hashing
{
    /// <summary>
    /// Implements a family of randomized Hash Functions
    /// </summary>
    public class HashingFamily
    {
        private ArrayList<int> _multipliersVector { get; set; }
        private Random _randomNumbers { get; set; }

        /// <summary>
        /// Initializes the family with a specified number of hash functions.
        /// </summary>
        public HashingFamily(int numberOfHashFunctions)
        {
            if (numberOfHashFunctions <= 0)
                throw new ArgumentOutOfRangeException("Number of hash functions should be greater than zero.");

            this._multipliersVector = new ArrayList<int>(numberOfHashFunctions);
            this._randomNumbers = new Random();
        }

        /// <summary>
        /// Returns number of member hash functions.
        /// </summary>
        public int NumberOfFunctions
        {
            get { return this._multipliersVector.Count; }
        }

        /// <summary>
        /// Generates new hash functions with new randomized multipliers.
        /// </summary>
        public void GenerateNewFunctions()
        {
            for(int i = 0; i < _multipliersVector.Count; i++)
            {
                _multipliersVector[i] = _randomNumbers.Next(1, Int32.MaxValue);
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
            if (whichHashFunction <= 0 || whichHashFunction > _multipliersVector.Count)
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
            if (whichHashFunction <= 0 || whichHashFunction > _multipliersVector.Count)
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
