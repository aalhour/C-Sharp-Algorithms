using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Dictionaries
{
    /// <summary>
    /// Hash Table with Chaining.
    /// </summary>
    public class HashTable_SeparateChaining<TKey, TValue> : IDictionary<TKey, TValue> where TKey : IComparable<TKey>
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
        private decimal _occupiedSlotsLoadRatio;
        private const int _defaultCapacity = 7;
        private DLinkedList<TKey, TValue>[] _keysValuesMap;
        private static readonly DLinkedList<TKey, TValue>[] _emptyArray = new DLinkedList<TKey, TValue>[7];
        private List<TKey> _keysCollection { get; set; }
        private List<TValue> _valuesCollection { get; set; }
        
        // Keys and Values Comparers
        private EqualityComparer<TKey> _keysComparer { get; set; }
        private EqualityComparer<TValue> _valuesComparer { get; set; }
        
        // A collection of prime numbers to use as hash table sizes. 
        internal static readonly int[] _primes = {
            //2, 7, 11, 17, 23, 29, 37, 47, 59, 71, 89, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            7, 17, 29, 59, 107, 131, 163, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369 };

        // Random numbers generator.
        private Random _randomNumberGenerator { get; set; }

        // This is the maximum prime that is smaller than the C# maximum allowed size of arrays.
        // Check the following reference: 
        // C# Maximum Array Length (before encountering overflow).
        // Link: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
        private const int MaxPrimeArrayLength = 0x7FEFFFFD;

        // Picked the HashPrime to be (101) because it is prime, and if the ‘hashSize - 1’ is not a multiple of this HashPrime, which is 
        // enforced in _getUpperBoundPrime, then expand function has the potential of being every value from 1 to hashSize - 1. 
        // The choice is largely arbitrary.
        private const int HashPrime = 101;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        public HashTable_SeparateChaining()
        {
            this._size = 0;
            this._keysValuesMap = _emptyArray;
            this._freeSlotsCount = this._keysValuesMap.Length;
            this._keysComparer = EqualityComparer<TKey>.Default;
            this._valuesComparer = EqualityComparer<TValue>.Default;
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

            return _getUpperBoundPrime(newSize);
        }

        /// <summary>
        /// Contracts the capacity of the keys and values arrays.
        /// </summary>
        private void _contractCapacity()
        {
            int oneThird = (_keysValuesMap.Length / 3);

            if (_size <= oneThird)
            {
                int newCapacity = (_keysValuesMap.Length == 0 ? _defaultCapacity : _getContractPrime(_keysValuesMap.Length));

                //
                // Try to expand the size
                DLinkedList<TKey, TValue>[] newKeysMap =
                    new DLinkedList<TKey, TValue>[newCapacity];

                if (_size > 0)
                {
                    // Reset the free slots count
                    _freeSlotsCount = newKeysMap.Length;

                    for (int i = 0; i < _keysValuesMap.Length; ++i)
                    {
                        var chain = _keysValuesMap[i];

                        if (chain != null && chain.Count > 0)
                        {
                            var head = chain.Head;

                            while (head != null)
                            {
                                int hash = _getHashOfKey(head.Key, newKeysMap.Length);

                                if (newKeysMap[hash] == null)
                                {
                                    _freeSlotsCount--;
                                    newKeysMap[hash] = new DLinkedList<TKey, TValue>();
                                }

                                newKeysMap[hash].Append(head.Key, head.Value);

                                head = head.Next;
                            }
                        }
                    }//end-for
                }//end-if

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
                    newCapacity = MaxPrimeArrayLength;

                // Try to expand the size
                try
                {
                    DLinkedList<TKey, TValue>[] newKeysMap =
                        new DLinkedList<TKey, TValue>[newCapacity];

                    if (_size > 0)
                    {
                        // Reset the free slots count
                        _freeSlotsCount = newKeysMap.Length;

                        for(int i = 0; i < _keysValuesMap.Length; ++i)
                        {
                            var chain = _keysValuesMap[i];
                            
                            if(chain != null && chain.Count > 0)
                            {
                                var head = chain.Head;
                                
                                while(head != null)
                                {
                                    int hash = _getHashOfKey(head.Key, newKeysMap.Length);

                                    if (newKeysMap[hash] == null)
                                    {
                                        _freeSlotsCount--;
                                        newKeysMap[hash] = new DLinkedList<TKey, TValue>();
                                    }
                                    
                                    newKeysMap[hash].Append(head.Key, head.Value);

                                    head = head.Next;
                                }
                            }
                        }//end-for
                    }//end-if

                    _keysValuesMap = newKeysMap;
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
            int oneThird = (_keysValuesMap.Length / 3);

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

        /// <summary>
        /// Returns a key from 0 to m where m is the size of the keys-and-values map. The hash serves as an index.
        /// This is based on the Simple Universal Hashing Principle.
        /// </summary>
        private int _universalHashingPrinciple(TKey key, int length = -1)
        {
            int hashcode, a, b, primeNum;

            if (length == -1)
                length = _keysValuesMap.Length;

            primeNum = _primes[_primes.Length - 1];
            a = _randomNumberGenerator.Next(0, (primeNum - 1) / 2);
            b = _randomNumberGenerator.Next((primeNum) / 2, primeNum - 1);

            hashcode = Math.Abs((((a * _getPreHashOfKey(key)) + b) % primeNum) % _keysValuesMap.Length);

            return hashcode;
        }

        /// <summary>
        /// Returns a key from 0 to m where m is the size of the keys-and-values map. The hash serves as an index.
        /// Division Method.
        /// </summary>
        private int _getHashOfKey(TKey key, int length = -1)
        {
            if (length <= 0)
                length = _keysValuesMap.Length;

            int prehash = _getPreHashOfKey(key);
            int hash = prehash % length;

            return hash;
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
            int hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                return _keysValuesMap[hashcode].ContainsKey(key);
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
            int hashcode = _getHashOfKey(item.Key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                try
                {
                    var existingPair = _keysValuesMap[hashcode].Find(item.Key);

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
            int hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                try
                {
                    var existingPair = _keysValuesMap[hashcode].Find(key);
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
                int hashcode = _getHashOfKey(key);

                // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
                if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
                {
                    try
                    {
                        var existingPair = _keysValuesMap[hashcode].Find(key);
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
                int hashcode = _getHashOfKey(key);

                // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
                if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
                {
                    bool exists = _keysValuesMap[hashcode].ContainsKey(key);

                    if (exists == true)
                        _keysValuesMap[hashcode].UpdateValueByKey(key, value);
                }

                // NOT FOUND
                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Copy the key-value pairs in the hash table to an array starting from the specified index.
        /// </summary>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                array = new KeyValuePair<TKey, TValue>[_size];

            int countOfElements = (array.Length - arrayIndex);
            int hashTableIndex = 0;

            for(int i = arrayIndex; i < array.Length; ++i)
            {
                KeyValuePair<TKey, TValue> pair;

                if (_keysValuesMap[hashTableIndex] != null && _keysValuesMap[hashTableIndex].Count > 0)
                {
                    if (_keysValuesMap[hashTableIndex].Count == 1)
                    {
                        pair = new KeyValuePair<TKey, TValue>(_keysValuesMap[hashTableIndex].First.Key, _keysValuesMap[hashTableIndex].First.Value);
                        array[i] = pair;
                        hashTableIndex++;
                    }
                    else
                    {
                        var headOfChain = _keysValuesMap[hashTableIndex].Head;

                        while(hashTableIndex < countOfElements && i < array.Length)
                        {
                            pair = new KeyValuePair<TKey, TValue>(_keysValuesMap[hashTableIndex].First.Key, _keysValuesMap[hashTableIndex].First.Value);
                            array[i] = pair;
                            i++;
                            hashTableIndex++;
                        }
                    }
                }//end-if
            }//end-for
        }

        /// <summary>
        /// Add a key and value to the hash table.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            // Get hash of the key
            int hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] == null)
            {
                // This is an empty slot. Initialize the chain of collisions.
                _keysValuesMap[hashcode] = new DLinkedList<TKey, TValue>();

                // Decrease the number of free slots.
                _freeSlotsCount--;
            }
            else if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                if (_keysValuesMap[hashcode].ContainsKey(key) == true)
                    throw new ArgumentException("Key already exists in the hash table.");
            }

            _keysValuesMap[hashcode].Append(key, value);
            _size++;

            //Add the key-value to the keys and values collections
            _keysCollection.Add(key);
            _valuesCollection.Add(value);

            _occupiedSlotsLoadRatio = Decimal.Divide(
                Convert.ToDecimal(_size),
                Convert.ToDecimal(_keysValuesMap.Length));

            // Capacity management
            if (_occupiedSlotsLoadRatio.IsGreaterThanOrEqualTo(Convert.ToDecimal(0.90)))
            {
                _ensureCapacity(CapacityManagementMode.Expand, _keysValuesMap.Length + 1);
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
            int hashcode = _getHashOfKey(key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                try
                {
                    var keyValuePair = _keysValuesMap[hashcode].Find(key);

                    if (keyValuePair.Key.IsEqualTo(key))
                    {
                        _keysValuesMap[hashcode].RemoveBy(key);
                        _size--;

                        // check if no other keys exist in this slot.
                        if (_keysValuesMap[hashcode].Count == 0)
                        {
                            // Nullify the chain of collisions at this slot.
                            _keysValuesMap[hashcode] = null;

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
            int hashcode = _getHashOfKey(item.Key);

            // The chain of colliding keys are found at _keysValuesMap[hashcode] as a doubly-linked-list.
            if (_keysValuesMap[hashcode] != null && _keysValuesMap[hashcode].Count > 0)
            {
                try
                {
                    var keyValuePair = _keysValuesMap[hashcode].Find(item.Key);

                    if (keyValuePair.Key.IsEqualTo(item.Key) && _valuesComparer.Equals(keyValuePair.Value, item.Value))
                    {
                        _keysValuesMap[hashcode].RemoveBy(item.Key);
                        _size--;

                        // check if no other keys exist in this slot.
                        if (_keysValuesMap[hashcode].Count == 0)
                        {
                            // Nullify the chain of collisions at this slot.
                            _keysValuesMap[hashcode] = null;

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
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _size = 0;
            _keysValuesMap = _emptyArray;
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
