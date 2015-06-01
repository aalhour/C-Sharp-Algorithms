using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// Hash Table with Chaining.
    /// </summary>
    public class HashTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        private int _size;
        private DLinkedList<int>[] _keysValuesMap;
        private const int _defaultCapacity = 8;
        private static readonly DLinkedList<int>[] _emptyArray = new DLinkedList<int>[8];

        // A collection of prime numbers to use as hash table sizes. 
        internal static readonly int[] _primes = {
            3, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};

        // Random numbers generator.
        private Random _randomNumberGenerator { get; set; }

        // Keys Comparer
        private IEqualityComparer<TKey> _comparer { get; set; }

        // This sets the default maximum array length to refer to MAXIMUM_ARRAY_LENGTH_x64
        // Set the flag IsMaximumCapacityReached to false
        private bool DefaultMaxCapacityIsX64 = true;

        // This is the maximum prime that is smaller than the C# maximum allowed size of arrays.
        // Check the following reference: 
        // C# Maximum Array Length (before encountering overflow).
        // Link: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        private const int MaxPrimeArrayLength = 0x7FEFFFFD;

        // Picked the HashPrime to be (101) because it is prime, and if the ‘hashSize - 1’ is not a multiple of this HashPrime, which is 
        // enforced in _getUpperBoundPrime, then expand function has the potential of being every value from 1 to hashSize - 1. 
        // The choice is largely arbitrary.
        private const int HashPrime = 101;

        
        public HashTable()
        {
            this._size = 0;
            this._keysValuesMap = _emptyArray;
            this._randomNumberGenerator = new Random(Int32.MaxValue / 2);
        }


        /// <summary>
        /// Returns a prime number that is greater than the specified number.
        /// </summary>
        private int _getUpperBoundPrime(int minNumber)
        {
            if (minNumber < 0)
                throw new ArgumentException("Number should be greater than or equal to 0.");
            
            for (int i = 0; i < _primes.Length; i++)
            {
                int prime = _primes[i];

                if (prime >= minNumber)
                    return prime;
            }

            // Outside of our predefined table. Compute the prime the hard way. 
            for (int i = (minNumber | 1); i < Int32.MaxValue; i += 2)
            {
                if (Helpers.IsPrime(i) && ((i - 1) % HashPrime != 0))
                    return i;
            }

            return minNumber;
        }

        /// <summary>
        /// Returns the next biggest prime that is greater than twice the size of the interal array (size * 2).
        /// </summary>
        private int _getExpandPrime(int oldSize)
        {
            int newSize = 2 * oldSize;

            // Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint)newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize)
            {
                return MaxPrimeArrayLength;
            }

            return _getUpperBoundPrime(newSize);
        }

        /// <summary>
        /// Get the next smaller prime that is smaller than half the size of the internal array (size / 2);
        /// </summary>
        /// <param name="oldSize"></param>
        /// <returns></returns>
        private int _getContractPrime(int oldSize)
        {
            int newSize = oldSize / 2;

            // Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint)newSize > MaxPrimeArrayLength && MaxPrimeArrayLength > oldSize)
            {
                return MaxPrimeArrayLength;
            }

            return _getUpperBoundPrime(newSize);
        }

        /// <summary>
        /// Contracts the capacity of the keys and values arrays.
        /// </summary>
        private void _contractCapacity()
        {
            int twoThirds = 2 * (_keysValuesMap.Length / 3);

            if (_size <= _keysValuesMap.Length * 2 / 3)
            {
                int newCapacity = (_keysValuesMap.Length == 0 ? _defaultCapacity : _getContractPrime(_keysValuesMap.Length));

                //
                // Try to expand the size
                DLinkedList<int>[] newKeysMap = new DLinkedList<int>[newCapacity];

                if (_size > 0)
                {
                    Array.Copy(_keysValuesMap, 0, newKeysMap, 0, _size);
                }

                _keysValuesMap = newKeysMap;
            }
        }

        /// <summary>
        /// Expands the capacity of the keys and values arrays.
        /// </summary>
        private void _expandCapacity(int minCapacity)
        {
            if (_keysValuesMap.Length < minCapacity)
            {
                int newCapacity = (_keysValuesMap.Length == 0 ? _defaultCapacity : _getExpandPrime(_keysValuesMap.Length));

                if (newCapacity >= MaxPrimeArrayLength)
                {
                    newCapacity = MaxPrimeArrayLength;
                }
                else if (newCapacity < minCapacity)
                {
                    newCapacity = minCapacity;
                }

                // Try to expand the size
                try
                {
                    DLinkedList<int>[] newKeysMap = new DLinkedList<int>[newCapacity];

                    if (_size > 0)
                    {
                        Array.Copy(_keysValuesMap, 0, newKeysMap, 0, _size);
                    }

                    _keysValuesMap = newKeysMap;
                }
                catch (OutOfMemoryException)
                {
                    if (DefaultMaxCapacityIsX64 == true)
                    {
                        DefaultMaxCapacityIsX64 = false;
                        _expandCapacity(newCapacity);
                    }

                    throw;
                }
            }
        }

        /// <summary>
        /// Returns an integer that represents the key.
        /// Used in the _hashKey function.
        /// </summary>
        private int _getPreHashOfKey(TKey key)
        {
            //if (true == key.IsNumber())
            //{
            //    return Convert.ToInt32(key);
            //}
            //else
            //{
            //    if (key.IsEqualTo(default(TKey)))
            //        return ((Object)key).GetHashCode();
            //    else
            //        return key.GetHashCode();
            //}

            return _comparer.GetHashCode(key);
        }

        /// <summary>
        /// Returns a key from 0 to m where m is the size of the keys-and-values map. The hash serves as an index.
        /// This is based on the Simple Universal Hashing Principle.
        /// </summary>
        private int _getHashOfKey(TKey key)
        {
            int hashcode, a, b;

            //a = _randomNumberGenerator.Next(_keysValuesMap.Length, (Int32.MaxValue / 2 - 1));
            //b = _randomNumberGenerator.Next((Int32.MaxValue / 2  + 1), Int32.MaxValue);
            a = _randomNumberGenerator.Next();
            b = _randomNumberGenerator.Next();

            hashcode = Math.Abs((((a * _getPreHashOfKey(key)) + b) % _primes[key.ToString().ToCharArray().Length]) % _keysValuesMap.Length);

            return hashcode;
        }


        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public ICollection<TKey> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public ICollection<TValue> Values
        {
            get { throw new NotImplementedException(); }
        }

        public TValue this[TKey key]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
