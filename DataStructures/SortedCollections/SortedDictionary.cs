using System;
using System.Collections.Generic;

using DataStructures.Common;
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
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }

        public void Add(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            throw new NotImplementedException();
        }

        public TValue this[TKey index]
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

        public ICollection<TKey> Keys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ICollection<TValue> Values
        {
            get
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
            _collection = new RedBlackTreeMap<TKey, TValue>(allowDuplicates: false);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotImplementedException();
        }


        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

