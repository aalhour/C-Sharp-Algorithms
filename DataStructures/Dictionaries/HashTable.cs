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
		private DLinkedList<int>[] _keysMap;
        //private DLinkedList<int>[] _valuesMap;
		private const int _defaultCapacity = 8;
        private static readonly DLinkedList<int>[] _emptyArray = new DLinkedList<int>[8];

		// This sets the default maximum array length to refer to MAXIMUM_ARRAY_LENGTH_x64
		// Set the flag IsMaximumCapacityReached to false
		private bool DefaultMaxCapacityIsX64 = true;
		private bool IsMaximumCapacityReached = false;

		// The C# Maximum Array Length (before encountering overflow)
		// Reference: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
		private const int MAXIMUM_ARRAY_LENGTH_x64 = 0X7FEFFFFF; //x64
		private const int MAXIMUM_ARRAY_LENGTH_x86 = 0x8000000; //x86


		public HashTable()
		{
			this._size = 0;
			this._keysMap = _emptyArray;
            //this._valuesMap = _emptyArray;
		}


		/// <summary>
		/// Contracts the capacity of the keys and values arrays.
		/// </summary>
		private void _contractCapacity()
		{
			int twoThirds = 2 * (_keysMap.Length / 3);

			if (_size <= _keysMap.Length * 2 / 3)
			{
				int newCapacity = (_keysMap.Length == 0 ? _defaultCapacity : _keysMap.Length / 2);

				//
				// Try to expand the size
                DLinkedList<int>[] newKeysMap = new DLinkedList<int>[newCapacity];
                //DLinkedList<int>[] newValuesMap = new DLinkedList<int>[newCapacity];

				if (_size > 0)
				{
					Array.Copy(_keysMap, 0, newKeysMap, 0, _size);
                    //Array.Copy(_valuesMap, 0, newValuesMap, 0, _size);
				}

				_keysMap = newKeysMap;
                //_valuesMap = newValuesMap;
			}
		}

		/// <summary>
		/// Expands the capacity of the keys and values arrays.
		/// </summary>
		private void _expandCapacity(int minCapacity)
		{
			if (_keysMap.Length < minCapacity)
			{
				int newCapacity = (_keysMap.Length == 0 ? _defaultCapacity : _keysMap.Length * 2);

				// Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
				// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
				int maxCapacity = (DefaultMaxCapacityIsX64 == true ? MAXIMUM_ARRAY_LENGTH_x64 : MAXIMUM_ARRAY_LENGTH_x86);

				if (newCapacity >= maxCapacity)
				{
					newCapacity = maxCapacity;
					IsMaximumCapacityReached = true;
				}
				else if (newCapacity < minCapacity)
				{
					newCapacity = minCapacity;
				}


				//
				// Try to expand the size
				try
				{
                    DLinkedList<int>[] newKeysMap = new DLinkedList<int>[newCapacity];
                    //int[] newValuesMap = new int[newCapacity];

					if (_size > 0)
					{
						Array.Copy(_keysMap, 0, newKeysMap, 0, _size);
                        //Array.Copy(_valuesMap, 0, newValuesMap, 0, _size);
					}

					_keysMap = newKeysMap;
                    //_valuesMap = newValuesMap;
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
