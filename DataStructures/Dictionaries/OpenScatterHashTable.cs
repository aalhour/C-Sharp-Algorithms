using System;
using System.Collections.Generic;
using DataStructures.Common;

namespace DataStructures.Dictionaries
{

    /// <summary>
    /// Hash Table with Open Addressing.
    /// </summary>
    public class OpenScatterHashTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Hash Table Cell.
        /// </summary>
        private class HashTableEntry<TKey, TValue> where TKey : IComparable<TKey>
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public EntryStatus Status { get; set; }

            public HashTableEntry() : this(default(TKey), default(TValue)) { }
            public HashTableEntry(TKey key, TValue value, EntryStatus status = EntryStatus.Occupied)
            {
                Key = key;
                Value = value;
                Status = status;
            }

            public bool IsEmpty { get { return this.Status == EntryStatus.Empty; } }

            public bool IsOccupied { get { return this.Status == EntryStatus.Occupied; } }

            public bool IsDeleted { get { return this.Status == EntryStatus.Deleted; } }
        }



        /// <summary>
        /// INSTANCE VARIABLES
        /// </summary>
        private int _size;
        private decimal _loadFactor;
        private HashTableEntry<TKey, TValue>[] _hashTableStore;

        // Initialization-related
        private const int _defaultCapacity = 7;
        private static readonly HashTableEntry<TKey, TValue>[] _emptyArray = new HashTableEntry<TKey, TValue>[_defaultCapacity];

        // Helper collections.
        private List<TKey> _keysCollection { get; set; }
        private List<TValue> _valuesCollection { get; set; }

        // Keys and Values Comparers
        private EqualityComparer<TKey> _keysComparer { get; set; }
        private EqualityComparer<TValue> _valuesComparer { get; set; }

        // A collection of prime numbers to use as hash table sizes. 
        internal static readonly PrimesList _primes = PrimesList.Instance;

        // This is the maximum prime that is smaller than the C# maximum allowed size of arrays.
        // Check the following reference: 
        // C# Maximum Array Length (before encountering overflow).
        // Link: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        private const int MAX_PRIME_ARRAY_LENGTH = 0x7FEFFFFD;

        // Picked the HashPrime to be (101) because it is prime, and if the ‘hashSize - 1’ is not a multiple of this HashPrime, which is 
        // enforced in _getUpperBoundPrime, then expand function has the potential of being every value from 1 to hashSize - 1. 
        // The choice is largely arbitrary.
        private const int HASH_PRIME = 101;

        /// <summary>
        /// The hash table cell status modes: Empty cell, Occupied cell, Deleted cell.
        /// </summary>
        private enum EntryStatus { Empty = 0, Occupied = 1, Deleted = 2 }

        /// <summary>
        /// Used in the ensure capacity function
        /// </summary>
        private enum CapacityManagementMode { Contract = 0, Expand = 1 }


        /// <summary>
        /// Returns the next biggest prime that is greater than twice the size of the interal array (size * 2).
        /// </summary>
        private int _getExpandPrime(int oldSize)
        {
            int newSize = 2 * oldSize;

            // Allow the hashtables to grow to maximum possible size (~2G elements) before encoutering capacity overflow.
            // Note that this check works even when _items.Length overflowed thanks to the (uint) cast
            if ((uint)newSize > MAX_PRIME_ARRAY_LENGTH && MAX_PRIME_ARRAY_LENGTH > oldSize)
            {
                return MAX_PRIME_ARRAY_LENGTH;
            }

            return _primes.GetNextPrime(newSize);
        }

        /// <summary>
        /// Get the next smaller prime that is smaller than half the size of the internal array (size / 2);
        /// </summary>
        private int _getContractPrime(int oldSize)
        {
            int newSize = oldSize / 2;

            return _primes.GetNextPrime(newSize);
        }

        /// <summary>
        /// Contracts the capacity of the keys and values arrays.
        /// </summary>
        private void _contractCapacity()
        {
            // Only contract the array if the number of elements is less than 1/3 of the total array size.
            int oneThird = (_hashTableStore.Length / 3);

            if (_size <= oneThird)
            {
                int newCapacity = (_hashTableStore.Length == 0 ? _defaultCapacity : _getContractPrime(_hashTableStore.Length));

                // Try to expand the size
                HashTableEntry<TKey, TValue>[] newKeysMap = new HashTableEntry<TKey, TValue>[newCapacity];

                if (_size > 0)
                {
                    // REHASH
                }

                _hashTableStore = newKeysMap;
            }
        }

        /// <summary>
        /// Expands the capacity of the keys and values arrays.
        /// </summary>
        private void _expandCapacity(int minCapacity)
        {
            if (_hashTableStore.Length < minCapacity)
            {
                int newCapacity = (_hashTableStore.Length == 0 ? _defaultCapacity : _getExpandPrime(_hashTableStore.Length * 2));

                if (newCapacity >= MAX_PRIME_ARRAY_LENGTH)
                    newCapacity = MAX_PRIME_ARRAY_LENGTH;

                // Try to expand the size
                try
                {
                    HashTableEntry<TKey, TValue>[] newKeysMap = new HashTableEntry<TKey, TValue>[newCapacity];

                    if (_size > 0)
                    {
                        // REHASH
                    }

                    _hashTableStore = newKeysMap;
                }
                catch (OutOfMemoryException)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// A high-level functon that handles both contraction and expansion of the internal collection.
        /// </summary>
        /// <param name="mode">Contract or Expand.</param>
        /// <param name="newSize">The new expansion size.</param>
        private void _ensureCapacity(CapacityManagementMode mode, int newSize = -1)
        {
            // If the size of the internal collection is less than or equal to third of 
            // ... the total capacity then contract the internal collection
            int oneThird = (_hashTableStore.Length / 3);

            if (mode == CapacityManagementMode.Contract && _size <= oneThird)
            {
                _contractCapacity();
            }
            else if (mode == CapacityManagementMode.Expand && newSize > 0)
            {
                _expandCapacity(newSize);
            }
        }

        /// <summary>
        /// Returns an integer that represents the key.
        /// Used in the _hashKey function.
        /// </summary>
        private int _getPreHashOfKey(TKey key)
        {
            return Math.Abs(_keysComparer.GetHashCode(key));
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
