/***
 * Cuckoo Hash Table.
 * 
 * A hash table that implements the Cuckoo Hashing algorithm for resolving keys-collisions. 
 * This is a single-table implementation, the source behind this idea is the work of Mark Allen Weiss, 2014.
 */

using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Hashing;
using System.Threading.Tasks;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// THE CUCKOO HASH TABLE Data Structure.
    /// </summary>
    public class CuckooHashTable<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// THE CUCKOO HASH TABLE ENTERY
        /// </summary>
        private class CHashEntry<TKey, TValue> where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public bool IsActive { get; set; }

            public CHashEntry() : this(default(TKey), default(TValue), false) { }

            public CHashEntry(TKey key, TValue value, bool isActive)
            {
                Key = key;
                Value = value;
                IsActive = isActive;
            }
        }


        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private const int DEFAULT_CAPACITY = 11;
        private const double MAX_LOAD_FACTOR = 0.45;
        private const int ALLOWED_REHASHES = 5;
        private const int NUMBER_OF_HASH_FUNCTIONS = 7; // number of hash functions to use, selected 7 because it's prime. The choice was arbitrary.
        internal readonly PrimesList PRIMES = PrimesList.Instance;

        // Random number generator
        private Random _randomizer;

        private int _size { get; set; }
        private int _numberOfRehashes { get; set; }
        private CHashEntry<TKey, TValue>[] _collection { get; set; }
        private UniversalHashingFamily _universalHashingFamily { get; set; }
        private EqualityComparer<TKey> _equalityComparer = EqualityComparer<TKey>.Default;

        // The C# Maximum Array Length (before encountering overflow)
        // Reference: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        private const int MAX_ARRAY_LENGTH = 0X7FEFFFFF;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public CuckooHashTable()
        {
            _size = 0;
            _numberOfRehashes = 0;
            _randomizer = new Random();
            _collection = new CHashEntry<TKey, TValue>[DEFAULT_CAPACITY];
            _universalHashingFamily = new UniversalHashingFamily(NUMBER_OF_HASH_FUNCTIONS);
        }

        /// <summary>
        /// Expands the size of internal collection.
        /// </summary>
        private void _expandCapacity(int minCapacity)
        {
            int newCapacity = (_collection.Length == 0 ? DEFAULT_CAPACITY : _collection.Length * 2);

            // Handle overflow
            if (newCapacity >= MAX_ARRAY_LENGTH)
                newCapacity = MAX_ARRAY_LENGTH;
            else if (newCapacity < minCapacity)
                newCapacity = minCapacity;

            _rehash(Convert.ToInt32(newCapacity));
        }

        /// <summary>
        /// Contracts the size of internal collection to half.
        /// </summary>
        private void _contractCapacity()
        {
            _rehash(_size / 2);
        }

        /// <summary>
        /// Rehashes the internal internal collection.
        /// Table size stays the same, but generates new hash functions.
        /// </summary>
        private void _rehash()
        {
            _universalHashingFamily.GenerateNewFunctions();
            _rehash(_collection.Length);
        }

        /// <summary>
        /// Rehashes the internal collection to a new size.
        /// New hash table size, but the hash functions stay the same.
        /// </summary>
        private void _rehash(int newCapacity)
        {
            int primeCapacity = PRIMES.GetNextPrime(newCapacity);

            var oldSize = _size;
            var oldCollection = this._collection;

            try
            {
                this._collection = new CHashEntry<TKey, TValue>[newCapacity];

                // Reset size
                _size = 0;

                for (int i = 0; i < oldCollection.Length; ++i)
                {
                    if (oldCollection[i] != null && oldCollection[i].IsActive == true)
                    {
                        _insertHelper(oldCollection[i].Key, oldCollection[i].Value);
                    }
                }
            }
            catch (OutOfMemoryException ex)
            {
                // In case a memory overflow happens, return the data to it's old state
                // ... then throw the exception.
                _collection = oldCollection;
                _size = oldSize;

                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Hashes a key, using the specified hash function number which belongs to the internal hash functions family.
        /// </summary>
        private int _cuckooHash(TKey key, int whichHashFunction)
        {
            if (whichHashFunction <= 0 || whichHashFunction > _universalHashingFamily.NumberOfFunctions)
                throw new ArgumentOutOfRangeException("Which Hash Function parameter must be betwwen 1 and " + NUMBER_OF_HASH_FUNCTIONS + ".");

            int hashCode = Math.Abs(_universalHashingFamily.UniversalHash(_equalityComparer.GetHashCode(key), whichHashFunction));

            return hashCode % _collection.Length;
        }

        /// <summary>
        /// Checks whether there is an entry at the specified position and that the entry is active.
        /// </summary>
        private bool _isActive(int index)
        {
            if (index < 0 || index > _collection.Length)
                throw new IndexOutOfRangeException();

            return (_collection[index] != null && _collection[index].IsActive == true);
        }

        /// <summary>
        /// Returns the array position (index) of the specified key.
        /// </summary>
        private int _findPosition(TKey key)
        {
            // The hash functions numbers are indexed from 1 not zero
            for (int i = 1; i <= NUMBER_OF_HASH_FUNCTIONS; ++i)
            {
                int index = _cuckooHash(key, i);

                if (_isActive(index) && _collection[index].Key.IsEqualTo(key))
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// Inserts a key-value pair into hash table.
        /// </summary>
        private void _insertHelper(TKey key, TValue value)
        {
            int COUNT_LIMIT = 100;
            var newEntry = new CHashEntry<TKey, TValue>(key, value, isActive: true);

            while (true)
            {
                int position, lastPosition = -1;

                for (int count = 0; count < COUNT_LIMIT; count++)
                {
                    // The hash functions numbers are indexed from 1 not zero
                    for (int i = 1; i <= NUMBER_OF_HASH_FUNCTIONS; i++)
                    {
                        position = _cuckooHash(key, i);

                        if (!_isActive(position))
                        {
                            _collection[position] = newEntry;

                            // Increment size
                            ++_size;

                            return;
                        }
                    }

                    // Eviction strategy:
                    // No available spot was found. Choose a random one.
                    int j = 0;
                    do
                    {
                        position = _cuckooHash(key, _randomizer.Next(1, NUMBER_OF_HASH_FUNCTIONS));
                    } while (position == lastPosition && j++ < NUMBER_OF_HASH_FUNCTIONS);

                    // SWAP ENTRY
                    lastPosition = position;

                    var temp = _collection[position];
                    _collection[position] = newEntry;
                    newEntry = temp;

                }//end-for

                if (++_numberOfRehashes > ALLOWED_REHASHES)
                {
                    // Expand the table.
                    _expandCapacity(_collection.Length + 1);

                    // Reset number of rehashes.
                    _numberOfRehashes = 0;
                }
                else
                {
                    // Rehash the table with the same current size.
                    _rehash();
                }
            }//end-while
        }


        /// <summary>
        /// Returns number of items in hash table.
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return _size;
        }

        /// <summary>
        /// Returns true if hash table is empty; otherwise, false.
        /// </summary>
        public bool IsEmpty()
        {
            return (_size == 0);
        }

        /// <summary>
        /// Returns the value of the specified key, if exists; otherwise, raises an exception.
        /// </summary>
        public TValue this[TKey key]
        {
            get
            {
                int position = _findPosition(key);

                if (position != -1)
                    return _collection[position].Value;

                throw new KeyNotFoundException();
            }
            set
            {
                if (ContainsKey(key) == true)
                    Update(key, value);

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Checks if a key exists in the hash table.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return (_findPosition(key) != -1);
        }

        /// <summary>
        /// Insert key-value pair into hash table.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new Exception("Key already exists in the hash table.");

            if (_size >= _collection.Length * MAX_LOAD_FACTOR)
                _expandCapacity(_collection.Length + 1);

            _insertHelper(key, value);
        }

        /// <summary>
        /// Updates a key-value pair with a new value.
        /// </summary>
        public void Update(TKey key, TValue value)
        {
            int position = _findPosition(key);

            if (position == -1)
                throw new KeyNotFoundException();

            _collection[position].Value = value;
        }

        /// <summary>
        /// Remove the key-value pair specified by the given key.
        /// </summary>
        public bool Remove(TKey key)
        {
            int currentPosition = _findPosition(key);

            if (!_isActive(currentPosition))
                return false;

            // Mark the entry as not active
            _collection[currentPosition].IsActive = false;

            // Decrease the size
            --_size;

            return true;
        }

        /// <summary>
        /// Clears this hash table.
        /// </summary>
        public void Clear()
        {
            this._size = 0;

            Parallel.ForEach(_collection,
                (item) =>
                {
                    if (item != null && item.IsActive == true)
                    {
                        item.IsActive = false;
                    }
                });
        }
    }

}
