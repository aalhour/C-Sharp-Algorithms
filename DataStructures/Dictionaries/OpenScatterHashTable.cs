using System;
using System.Collections.Generic;
using DataStructures.Common;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// Hash Table with Open Addressing (Linear Probing).
    /// </summary>
    public class OpenScatterHashTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Hash Table Cell.
        /// </summary>
        private class HashTableEntry
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public EntryStatus Status { get; set; }

            public HashTableEntry() : this(default(TKey), default(TValue), EntryStatus.Empty) { }
            public HashTableEntry(TKey key, TValue value, EntryStatus status = EntryStatus.Occupied)
            {
                Key = key;
                Value = value;
                Status = status;
            }

            public bool IsEmpty { get { return Status == EntryStatus.Empty; } }
            public bool IsOccupied { get { return Status == EntryStatus.Occupied; } }
            public bool IsDeleted { get { return Status == EntryStatus.Deleted; } }
        }

        /// <summary>
        /// The hash table cell status modes.
        /// </summary>
        private enum EntryStatus { Empty = 0, Occupied = 1, Deleted = 2 }

        // Instance variables
        private int _size;
        private HashTableEntry[] _hashTableStore;
        private const int _defaultCapacity = 7;
        private const double _maxLoadFactor = 0.7;
        private const double _minLoadFactor = 0.2;

        // Comparers
        private readonly EqualityComparer<TKey> _keysComparer;

        // A collection of prime numbers to use as hash table sizes.
        private static readonly PrimesList _primes = PrimesList.Instance;

        /// <summary>
        /// Constructor.
        /// </summary>
        public OpenScatterHashTable()
        {
            _size = 0;
            _hashTableStore = new HashTableEntry[_defaultCapacity];
            _keysComparer = EqualityComparer<TKey>.Default;

            // Initialize all slots as empty
            for (int i = 0; i < _hashTableStore.Length; i++)
                _hashTableStore[i] = new HashTableEntry();
        }

        /// <summary>
        /// Constructor with initial capacity.
        /// </summary>
        public OpenScatterHashTable(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("capacity");

            int actualCapacity = _primes.GetNextPrime(Math.Max(capacity, _defaultCapacity));
            _size = 0;
            _hashTableStore = new HashTableEntry[actualCapacity];
            _keysComparer = EqualityComparer<TKey>.Default;

            for (int i = 0; i < _hashTableStore.Length; i++)
                _hashTableStore[i] = new HashTableEntry();
        }

        /// <summary>
        /// Computes the hash index for a key.
        /// </summary>
        private int _hash(TKey key)
        {
            int hashCode = _keysComparer.GetHashCode(key) & 0x7FFFFFFF;
            return hashCode % _hashTableStore.Length;
        }

        /// <summary>
        /// Finds the slot for a key. Returns the index if found, or -1 if not found.
        /// </summary>
        private int _findSlot(TKey key)
        {
            int index = _hash(key);
            int startIndex = index;

            do
            {
                var entry = _hashTableStore[index];

                if (entry.IsEmpty)
                    return -1; // Key not found

                if (entry.IsOccupied && _keysComparer.Equals(entry.Key, key))
                    return index; // Found

                index = (index + 1) % _hashTableStore.Length;
            }
            while (index != startIndex);

            return -1;
        }

        /// <summary>
        /// Finds a slot for insertion. Returns the index of an empty or deleted slot.
        /// </summary>
        private int _findInsertSlot(TKey key)
        {
            int index = _hash(key);
            int startIndex = index;
            int firstDeletedIndex = -1;

            do
            {
                var entry = _hashTableStore[index];

                if (entry.IsEmpty)
                    return firstDeletedIndex >= 0 ? firstDeletedIndex : index;

                if (entry.IsDeleted && firstDeletedIndex < 0)
                    firstDeletedIndex = index;

                if (entry.IsOccupied && _keysComparer.Equals(entry.Key, key))
                    return index; // Key already exists

                index = (index + 1) % _hashTableStore.Length;
            }
            while (index != startIndex);

            return firstDeletedIndex >= 0 ? firstDeletedIndex : -1;
        }

        /// <summary>
        /// Resizes the hash table.
        /// </summary>
        private void _resize(int newCapacity)
        {
            var oldStore = _hashTableStore;
            _hashTableStore = new HashTableEntry[newCapacity];

            for (int i = 0; i < _hashTableStore.Length; i++)
                _hashTableStore[i] = new HashTableEntry();

            _size = 0;

            // Rehash all existing entries
            foreach (var entry in oldStore)
            {
                if (entry != null && entry.IsOccupied)
                {
                    int index = _findInsertSlot(entry.Key);
                    _hashTableStore[index].Key = entry.Key;
                    _hashTableStore[index].Value = entry.Value;
                    _hashTableStore[index].Status = EntryStatus.Occupied;
                    _size++;
                }
            }
        }

        /// <summary>
        /// Expands the table if load factor is too high.
        /// </summary>
        private void _expandIfNeeded()
        {
            double loadFactor = (double)_size / _hashTableStore.Length;
            if (loadFactor >= _maxLoadFactor)
            {
                int newCapacity = _primes.GetNextPrime(_hashTableStore.Length * 2);
                _resize(newCapacity);
            }
        }

        /// <summary>
        /// Contracts the table if load factor is too low.
        /// </summary>
        private void _contractIfNeeded()
        {
            if (_hashTableStore.Length <= _defaultCapacity)
                return;

            double loadFactor = (double)_size / _hashTableStore.Length;
            if (loadFactor <= _minLoadFactor)
            {
                int newCapacity = _primes.GetNextPrime(_hashTableStore.Length / 2);
                newCapacity = Math.Max(newCapacity, _defaultCapacity);
                _resize(newCapacity);
            }
        }

        #region IDictionary Implementation

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            _expandIfNeeded();

            int index = _findInsertSlot(key);
            if (index < 0)
                throw new InvalidOperationException("Hash table is full.");

            var entry = _hashTableStore[index];
            if (entry.IsOccupied && _keysComparer.Equals(entry.Key, key))
                throw new ArgumentException("An item with the same key has already been added.", "key");

            _hashTableStore[index].Key = key;
            _hashTableStore[index].Value = value;
            _hashTableStore[index].Status = EntryStatus.Occupied;
            _size++;
        }

        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            return _findSlot(key) >= 0;
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new List<TKey>(_size);
                foreach (var entry in _hashTableStore)
                {
                    if (entry != null && entry.IsOccupied)
                        keys.Add(entry.Key);
                }
                return keys;
            }
        }

        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            int index = _findSlot(key);
            if (index < 0)
                return false;

            _hashTableStore[index].Status = EntryStatus.Deleted;
            _hashTableStore[index].Key = default(TKey);
            _hashTableStore[index].Value = default(TValue);
            _size--;

            _contractIfNeeded();
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            int index = _findSlot(key);
            if (index >= 0)
            {
                value = _hashTableStore[index].Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = new List<TValue>(_size);
                foreach (var entry in _hashTableStore)
                {
                    if (entry != null && entry.IsOccupied)
                        values.Add(entry.Value);
                }
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                int index = _findSlot(key);
                if (index < 0)
                    throw new KeyNotFoundException("The given key was not present in the dictionary.");

                return _hashTableStore[index].Value;
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                _expandIfNeeded();

                int index = _findInsertSlot(key);
                if (index < 0)
                    throw new InvalidOperationException("Hash table is full.");

                bool isNewEntry = !_hashTableStore[index].IsOccupied || 
                                  !_keysComparer.Equals(_hashTableStore[index].Key, key);

                _hashTableStore[index].Key = key;
                _hashTableStore[index].Value = value;
                _hashTableStore[index].Status = EntryStatus.Occupied;

                if (isNewEntry)
                    _size++;
            }
        }

        #endregion

        #region ICollection Implementation

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _size = 0;
            _hashTableStore = new HashTableEntry[_defaultCapacity];
            for (int i = 0; i < _hashTableStore.Length; i++)
                _hashTableStore[i] = new HashTableEntry();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            int index = _findSlot(item.Key);
            if (index < 0)
                return false;

            return EqualityComparer<TValue>.Default.Equals(_hashTableStore[index].Value, item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (array.Length - arrayIndex < _size)
                throw new ArgumentException("The destination array is not large enough.");

            int j = arrayIndex;
            foreach (var entry in _hashTableStore)
            {
                if (entry != null && entry.IsOccupied)
                {
                    array[j++] = new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
                }
            }
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            int index = _findSlot(item.Key);
            if (index < 0)
                return false;

            if (!EqualityComparer<TValue>.Default.Equals(_hashTableStore[index].Value, item.Value))
                return false;

            _hashTableStore[index].Status = EntryStatus.Deleted;
            _hashTableStore[index].Key = default(TKey);
            _hashTableStore[index].Value = default(TValue);
            _size--;

            _contractIfNeeded();
            return true;
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var entry in _hashTableStore)
            {
                if (entry != null && entry.IsOccupied)
                {
                    yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
