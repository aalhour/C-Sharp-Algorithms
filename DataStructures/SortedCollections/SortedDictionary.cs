using System;
using System.Collections.Generic;

using DataStructures.Trees;

namespace DataStructures.SortedCollections
{
    /// <summary>
    /// Sorted Dictionary collection (Red-Black Tree based).
    /// </summary>
    public class SortedDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue> 
        where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The internal collection is a Red-Black Tree Map.
        /// </summary>
        private RedBlackTreeMap<TKey, TValue> _collection { get; set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        public SortedDictionary()
        {
            _collection = new RedBlackTreeMap<TKey, TValue>(allowDuplicates: false);
        }


        /// <summary>
        /// Gets the count of enteries in dictionary.
        /// </summary>
        public int Count
        {
            get { return _collection.Count; }
        }

        /// <summary>
        /// Returns true if dictionary is empty; otherwise, false.
        /// </summary>
        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Determines whether the current dictionary contains an entry with the specified key.
        /// </summary>
        public bool ContainsKey(TKey key)
        {
            return _collection.Contains(key);
        }

        /// <summary>
        /// Determines whether the current collection contains a specific key-value pair.
        /// </summary>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            try
            {
                var entry = _collection.Find(item.Key);
                return entry.Value.Equals(item.Value);
            }
            catch(Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Try to get the value of a key or default(TValue). Returns true if key exists; otherwise, false.
        /// </summary>
        public bool TryGetValue(TKey key, out TValue value)
        {
            // Set value to the default value of type TValue
            value = default(TValue);

            try
            {
                // Assign the returned object to value
                value = _collection.Find(key).Value;

                // return Success.
                return true;
            }
            catch(KeyNotFoundException)
            {
                // No entry was found with the specified key.
                // return Failure.
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the value at the specified key.
        /// </summary>
        public TValue this[TKey index]
        {
            get
            {
                // In case dictionary is empty
                if (IsEmpty)
                    throw new Exception("Dictionary is empty.");

                try
                {
                    return _collection.Find(index).Value;
                }
                catch(KeyNotFoundException)
                {
                    // Mask the tree's exception with a new one.
                    throw new KeyNotFoundException("Key doesn't exist in dictionary.");
                }
            }
            set
            {
                if (ContainsKey(index))
                    _collection.Update(index, value);
                else
                    Add(index, value);
            }
        }

        /// <summary>
        /// Gets the collection of keys in the dictionary.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                var keys = new System.Collections.Generic.List<TKey>(Count);
                var enumerator = _collection.GetInOrderEnumerator();

                while (enumerator.MoveNext())
                    keys.Add(enumerator.Current.Key);

                return keys;
            }
        }

        /// <summary>
        /// Gets the collection of values in the dictionary.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                var values = new System.Collections.Generic.List<TValue>(Count);
                var enumerator = _collection.GetInOrderEnumerator();

                while (enumerator.MoveNext())
                    values.Add(enumerator.Current.Value);

                return values;
            }
        }

        /// <summary>
        /// Add the specified key and value to the dictionary.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            // Throw an duplicate key exception if an entry with the same key exists
            try
            {
                _collection.Insert(key, value);
            }
            catch(InvalidOperationException)
            {
                throw new InvalidOperationException("An entry with the same key already exists in dictionary.");
            }
        }

        /// <summary>
        /// Removes the item with specific Key from the dictionary.
        /// </summary>
        public bool Remove(TKey key)
        {
            try
            {
                // Try removing it and return Success
                _collection.Remove(key);
                return true;
            }
            catch(Exception)
            {
                // Item was not found. Return Failure.
                return false;
            }
        }

        /// <summary>
        /// Add the key-value pair to the dictionary.
        /// </summary>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the first occurrence of an item from the current collection Key and Value will be matched.
        /// </summary>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (IsEmpty)
                return false;
            
            // Get the entry from collection
            var entry = _collection.Find(item.Key);

            // If the entry's value match the value of the specified item, remove it
            if (entry.Value.Equals(item.Value))
            {
                _collection.Remove(item.Key);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the key-value pairs to a given array starting from specified index.
        /// </summary>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException();

            var enumerator = _collection.GetInOrderEnumerator();

            while (enumerator.MoveNext() && arrayIndex < array.Length)
            {
                array[arrayIndex] = enumerator.Current;
                arrayIndex++;
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _collection = new RedBlackTreeMap<TKey, TValue>(allowDuplicates: false);
        }


        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _collection.GetInOrderEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

}

