/***
 * Chained Hash Table.
 * 
 * A hash table that implements the Separate-Chaining scheme for resolving keys-collisions. It also implements auto-resizing (expansion and contraction).
 */

using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// Hash Table with Chaining.
    /// </summary>
    public class ChainedHashTable<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// Used in the ensure capacity function
        /// </summary>
        public enum CapacityManagementMode
        {
            Contract = 0,
            Expand = 1
        }

        /// <summary>
        /// INSTANCE VARIABLES.
        /// </summary>
        private int _size;
        private int _freeSlotsCount;
        private decimal _slotsLoadFactor;
        private const int _defaultCapacity = 8;
        private DLinkedList<TKey, TValue>[] _hashTableStore;
        private List<TKey> _keysCollection { get; set; }
        private List<TValue> _valuesCollection { get; set; }

        // Keys and Values Comparers
        private EqualityComparer<TKey> _keysComparer { get; set; }
        private EqualityComparer<TValue> _valuesComparer { get; set; }

        // The C# Maximum Array Length (before encountering overflow)
        // Reference: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        private const int MAX_ARRAY_LENGTH = 0X7FEFFFFF;

        // Initial hash value.
        private const uint INITIAL_HASH = 0x9e3779b9;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public ChainedHashTable()
        {
            this._size = 0;
            this._hashTableStore = new DLinkedList<TKey, TValue>[_defaultCapacity];
            this._freeSlotsCount = this._hashTableStore.Length;
            this._keysComparer = EqualityComparer<TKey>.Default;
            this._valuesComparer = EqualityComparer<TValue>.Default;

            this._keysCollection = new List<TKey>();
            this._valuesCollection = new List<TValue>();
        }


        /// <summary>
        /// Rehash the the current collection elements to a new collection.
        /// </summary>
        private void _rehash(ref DLinkedList<TKey, TValue>[] newHashTableStore, int oldHashTableSize)
        {
            // Reset the free slots count
            this._freeSlotsCount = newHashTableStore.Length;

            for (int i = 0; i < oldHashTableSize; ++i)
            {
                var chain = _hashTableStore[i];

                if (chain != null && chain.Count > 0)
                {
                    var head = chain.Head;

                    while (head != null)
                    {
                        uint hash = _getHashOfKey(head.Key, newHashTableStore.Length);

                        if (newHashTableStore[hash] == null)
                        {
                            _freeSlotsCount--;
                            newHashTableStore[hash] = new DLinkedList<TKey, TValue>();
                        }

                        newHashTableStore[hash].Append(head.Key, head.Value);

                        head = head.Next;
                    }
                }
            }//end-for
        }

        /// <summary>
        /// Contracts the capacity of the keys and values arrays.
        /// </summary>
        private void _contractCapacity()
        {
            int oneThird = (_hashTableStore.Length / 3);
            int twoThirds = 2 * oneThird;

            if (_size <= oneThird)
            {
                int newCapacity = (_hashTableStore.Length == 0 ? _defaultCapacity : twoThirds);

                // Try to expand the size
                DLinkedList<TKey, TValue>[] newHashTableStore = new DLinkedList<TKey, TValue>[newCapacity];

                // Rehash
                if (_size > 0)
                {
                    _rehash(ref newHashTableStore, _hashTableStore.Length);
                }//end-if

                _hashTableStore = newHashTableStore;
            }
        }

        /// <summary>
        /// Expands the capacity of the keys and values arrays.
        /// </summary>
        private void _expandCapacity(int minCapacity)
        {
            if (_hashTableStore.Length < minCapacity)
            {
                int newCapacity = (_hashTableStore.Length == 0 ? _defaultCapacity : _hashTableStore.Length * 2);

                // Make sure it doesn't divide by 2 or 10
                if (newCapacity % 2 == 0 || newCapacity % 10 == 0)
                    newCapacity = newCapacity + 1;

                // Handle overflow
                if (newCapacity >= MAX_ARRAY_LENGTH)
                    newCapacity = MAX_ARRAY_LENGTH;
                else if (newCapacity < minCapacity)
                    newCapacity = minCapacity;

                // Try to expand the size
                try
                {
                    DLinkedList<TKey, TValue>[] newHashTableStore = new DLinkedList<TKey, TValue>[newCapacity];

                    // Rehash
                    if (_size > 0)
                    {
                        _rehash(ref newHashTableStore, _hashTableStore.Length);
                    }//end-if

                    _hashTableStore = newHashTableStore;
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
        /// Hash Function.
        /// The universal hashing principle method.
        /// </summary>
        private uint _universalHashFunction(TKey key, int length)
        {
            if (length < 0)
                throw new IndexOutOfRangeException();

            // Hashes
            uint prehash = 0, hash = INITIAL_HASH;

            // Primes
            int a = 197, b = 4049, p = 7199369;

            prehash = _getPreHashOfKey(key);
            hash = Convert.ToUInt32(((a * prehash + b) % p) % length);

            return hash;
        }

        /// <summary>
        /// Hash Function.
        /// The division method hashing.
        /// </summary>
        private uint _divisionMethodHashFunction(TKey key, int length)
        {
            uint prehash = 0, hash = INITIAL_HASH;

            if (length < 0)
                throw new IndexOutOfRangeException();

            if (key is string && key.IsEqualTo(default(TKey)) == false)
            {
                var stringKey = Convert.ToString(key);

                for (int i = 0; i < stringKey.Length; ++i)
                {
                    hash = (hash ^ stringKey[i]) + ((hash << 26) + (hash >> 6));
                }

                if (hash > length)
                    hash = Convert.ToUInt32(hash % length);
            }
            else
            {
                prehash = _getPreHashOfKey(key);
                hash = Convert.ToUInt32((37 * prehash) % length);
            }

            return hash;
        }

        /// <summary>
        /// Returns an integer that represents the key.
        /// Used in the _hashKey function.
        /// </summary>
        private uint _getPreHashOfKey(TKey key)
        {
            return Convert.ToUInt32(Math.Abs(_keysComparer.GetHashCode(key)));
        }

        /// <summary>
        /// Returns a key from 0 to m where m is the size of the keys-and-values map. The hash serves as an index.
        /// </summary>
        private uint _getHashOfKey(TKey key, int length)
        {
            return _universalHashFunction(key, length);
        }

        /// <summary>
        /// Returns a key from 0 to m where m is the size of the keys-and-values map. The hash serves as an index.
        /// Division Method.
        /// </summary>
        private uint _getHashOfKey(TKey key)
        {
            return _universalHashFunction(key, _hashTableStore.Length);
        }

        /// <summary>
        /// Return count of elements in the hash table.
        /// </summary>
        public int Count
        {
            get { return _size; }
        }

        /// <summary>
        /// Checks if the hash table is readonly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Checks if the hash table is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        /// <summary>
        /// Checks whether key exists in the hash table.
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
            {
                return _hashTableStore[hashcode].ContainsKey(key);
            }

            // else
            return false;
        }

        /// <summary>
        /// Checks whether a key-value pair exist in the hash table.
        /// </summary>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(item.Key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
            {
                try
                {
                    var existingPair = _hashTableStore[hashcode].Find(item.Key);

                    if (existingPair.Key.IsEqualTo(item.Key) && _valuesComparer.Equals(existingPair.Value, item.Value))
                        return true;
                }
                catch (KeyNotFoundException)
                {
                    // do nothing
                }
            }

            // else
            return false;
        }

        /// <summary>
        /// List of hash table keys.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get { return _keysCollection; }
        }

        /// <summary>
        /// List of hash table values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get { return _valuesCollection; }
        }


        /// <summary>
        /// Tries to get the value of key which might not be in the dictionary.
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
            {
                try
                {
                    var existingPair = _hashTableStore[hashcode].Find(key);
                    value = existingPair.Value;
                    return true;
                }
                catch (KeyNotFoundException)
                {
                    // do nothing
                }
            }

            // NOT FOUND
            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Gets or sets the value of a key.
        /// </summary>
        public TValue this[TKey key]
        {
            get
            {
                // Get hash of the key
                var hashcode = _getHashOfKey(key);

                // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
                if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
                {
                    try
                    {
                        var existingPair = _hashTableStore[hashcode].Find(key);
                        return existingPair.Value;
                    }
                    catch (KeyNotFoundException)
                    {
                        // do nothing
                    }
                }

                // NOT FOUND
                throw new KeyNotFoundException();
            }
            set
            {
                // Get hash of the key
                var hashcode = _getHashOfKey(key);

                // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
                if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
                {
                    bool exists = _hashTableStore[hashcode].ContainsKey(key);

                    if (exists == true)
                        _hashTableStore[hashcode].UpdateValueByKey(key, value);
                }

                // NOT FOUND
                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Add a key and value to the hash table.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] == null)
            {
                // This is an empty slot. Initialize the chain of collisions.
                _hashTableStore[hashcode] = new DLinkedList<TKey, TValue>();

                // Decrease the number of free slots.
                _freeSlotsCount--;
            }
            else if (_hashTableStore[hashcode].Count > 0)
            {
                if (_hashTableStore[hashcode].ContainsKey(key) == true)
                    throw new ArgumentException("Key already exists in the hash table.");
            }

            _hashTableStore[hashcode].Append(key, value);
            _size++;

            //Add the key-value to the keys and values collections
            _keysCollection.Add(key);
            _valuesCollection.Add(value);

            _slotsLoadFactor = Decimal.Divide(
                Convert.ToDecimal(_size),
                Convert.ToDecimal(_hashTableStore.Length));

            // Capacity management
            if (_slotsLoadFactor.IsGreaterThanOrEqualTo(Convert.ToDecimal(0.90)))
            {
                _ensureCapacity(CapacityManagementMode.Expand, _hashTableStore.Length + 1);
            }
        }

        /// <summary>
        /// Add a key-value pair to the hash table.
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Remove a key from the hash table and return the status.
        /// </summary>
        public bool Remove(TKey key)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
            {
                try
                {
                    var keyValuePair = _hashTableStore[hashcode].Find(key);

                    if (keyValuePair.Key.IsEqualTo(key))
                    {
                        _hashTableStore[hashcode].RemoveBy(key);
                        _size--;

                        // check if no other keys exist in this slot.
                        if (_hashTableStore[hashcode].Count == 0)
                        {
                            // Nullify the chain of collisions at this slot.
                            _hashTableStore[hashcode] = null;

                            // Increase the number of free slots.
                            _freeSlotsCount++;

                            // Capacity management
                            _ensureCapacity(CapacityManagementMode.Contract);
                        }

                        _keysCollection.Remove(key);
                        _valuesCollection.Remove(keyValuePair.Value);

                        return true;
                    }
                }
                catch
                {
                    // do nothing
                }
            }

            // else
            return false;
        }

        /// <summary>
        /// Remove a key-value pair from the hash table and return the status.
        /// </summary>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            // Get hash of the key
            var hashcode = _getHashOfKey(item.Key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_hashTableStore[hashcode] != null && _hashTableStore[hashcode].Count > 0)
            {
                try
                {
                    var keyValuePair = _hashTableStore[hashcode].Find(item.Key);

                    if (keyValuePair.Key.IsEqualTo(item.Key) && _valuesComparer.Equals(keyValuePair.Value, item.Value))
                    {
                        _hashTableStore[hashcode].RemoveBy(item.Key);
                        _size--;

                        // check if no other keys exist in this slot.
                        if (_hashTableStore[hashcode].Count == 0)
                        {
                            // Nullify the chain of collisions at this slot.
                            _hashTableStore[hashcode] = null;

                            // Increase the number of free slots.
                            _freeSlotsCount++;

                            // Capacity management
                            _ensureCapacity(CapacityManagementMode.Contract);
                        }

                        _keysCollection.Remove(keyValuePair.Key);
                        _valuesCollection.Remove(keyValuePair.Value);

                        return true;
                    }
                }
                catch
                {
                    // do nothing
                }
            }

            // else
            return false;
        }

        /// <summary>
        /// Copy the key-value pairs in the hash table to an array starting from the specified index.
        /// </summary>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                array = new KeyValuePair<TKey, TValue>[_size];

            int i = arrayIndex;
            int hashTableIndex = 0;
            var currentChainNode = new DLinkedListNode<TKey, TValue>();

            while (true)
            {
                KeyValuePair<TKey, TValue> pair;

                if (i >= array.Length)
                    break;

                if (_hashTableStore[hashTableIndex] != null)
                {
                    currentChainNode = _hashTableStore[hashTableIndex].Head;
                    while (currentChainNode != null && i < array.Length)
                    {
                        pair = new KeyValuePair<TKey, TValue>(currentChainNode.Key, currentChainNode.Value);
                        array[i] = pair;
                        i++;
                        hashTableIndex++;

                        currentChainNode = currentChainNode.Next;
                    }
                }
                else
                {
                    hashTableIndex++;
                }
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            // Clear the elements in the store
            Array.Clear(_hashTableStore, 0, _hashTableStore.Length);

            // Re-initialize to empty collection.
            _hashTableStore = new DLinkedList<TKey, TValue>[_defaultCapacity];

            _size = 0;
            _slotsLoadFactor = 0;
            _freeSlotsCount = _hashTableStore.Length;
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
