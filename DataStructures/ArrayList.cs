using System;
using System.Collections.Generic;

namespace DataStructures
{
	/// <summary>
	/// The Array-Based List Data Structure.
	/// </summary>
	public class ArrayList<T>
	{
		/// <summary>
		/// Instance variables.
		/// </summary>

		// The C# Maximum Array Length (before encountering overflow)
		// Reference: http://referencesource.microsoft.com/#mscorlib/system/array.cs,2d2b551eabe74985
		private const int MAXIMUM_ARRAY_LENGTH = 0X7FEFFFFF;

		// This is used as a default empty list initialization.
		private readonly T[] _emptyArray = new T[0];

		// The default capacity to resize to, when a minimum is lower than 5.
		private const int _defaultCapacity = 4;

		// The internal array of elements.
		private T[] _collection { get; set; }

		// This keeps track of the number of elements added to the array.
		// Serves as an index of last item + 1.
		private int _size { get; set; }


		/// <summary>
		/// CONSTRUCTORS
		/// </summary>
		public ArrayList ()
		{
			_size = 0;
			_collection = _emptyArray;
		}

		public ArrayList(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException ();
			}
			else if (capacity == 0)
			{
				_collection = _emptyArray;
			}
			else
			{
				_collection = new T[capacity];
			}

			_size = 0;
		}


		/// <summary>
		/// Ensures that the capacity of the list is greator or equal to a minimum value.
		/// </summary>
		/// <param name="minCapacity">Minimum capacity.</param>
		private void EnsureCapacity(int minCapacity)
		{
			if (_collection.Length < minCapacity)
			{
				int newCapacity = (_collection.Length == 0 ? _defaultCapacity : _collection.Length * 2);

				// Allow the list to grow to maximum possible capacity (~2G elements) before encountering overflow.
				// Note that this check works even when _items.Length overflowed thanks to the (uint) cast
				if ((uint)newCapacity > MAXIMUM_ARRAY_LENGTH)
				{
					newCapacity = MAXIMUM_ARRAY_LENGTH;
				}

				if (newCapacity < minCapacity)
				{
					newCapacity = minCapacity;
				}

				this.ResizeCapacity(newCapacity);
			}
		}


		/// <summary>
		/// Resizes the collection to a new maximum number of capacity.
		/// </summary>
		/// <param name="newCapacity">New capacity.</param>
		private void ResizeCapacity(int newCapacity)
		{
			if (newCapacity != _collection.Length && newCapacity > _size)
			{
				if (newCapacity > 0) 
				{
					T[] newCollection = new T[newCapacity];
				
					if (_size > 0) 
					{
						Array.Copy (_collection, 0, newCollection, 0, _size);
					}

					_collection = newCollection;
				}
				else
				{
					_collection = _emptyArray;
				}
			}
		}


		/// <summary>
		/// Gets the the number of elements in list.
		/// </summary>
		/// <value>Int.</value>
		public int Count
		{
			get
			{
				return _size;
			}
		}


		/// <summary>
		/// Gets or sets the item at the specified index.
		/// example: var a = list[0];
		/// example: list[0] = 1;
		/// </summary>
		/// <param name="index">Index.</param>
		public T this[int index]
		{
			get
			{
				if ((uint)index >= (uint)_size)
				{
					throw new IndexOutOfRangeException ();
				}

				return _collection [index];
			}

			set
			{
				if ((uint)index >= (uint)_size)
				{
					throw new IndexOutOfRangeException ();
				}

				_collection [index] = value;
			}
		}


		/// <summary>
		/// Add the specified dataItem to list.
		/// </summary>
		/// <param name="dataItem">Data item.</param>
		public void Add(T dataItem)
		{
			if (_size == _collection.Length)
			{
				EnsureCapacity (_size + 1);
			}

			_collection[_size++] = dataItem;
		}


		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			if (_size > 0)
			{
				Array.Clear (_collection, 0, _size);
				_size = 0;
			}
		}


		/// <summary>
		/// Checks whether the list contains the specified dataItem.
		/// </summary>
		/// <param name="dataItem">Data item.</param>
		public bool Contains(T dataItem)
		{
			// Null-value check
			if ((Object)dataItem == null)
			{
				for (int i = 0; i < _size; ++i)
				{
					if ((Object)_collection [i] == null) return true;
				}
			}
			else
			{
				// Construct a default equality comparer for this Type T
				// Use it to get the equal match for the dataItem
				EqualityComparer<T> comparer = EqualityComparer<T>.Default;

				for(int i=0; i<_size; ++i)
				{
					if(comparer.Equals(_collection[i], dataItem)) return true;
				}
			}

			return false;
		}


		/// <summary>
		/// Checks whether the list contains the specified dataItem.
		/// </summary>
		/// <param name="dataItem">Data item.</param>
		/// <param name="comparer">The Equality Comparer object.</param>
		public bool Contains(T dataItem, IEqualityComparer<T> comparer)
		{
			// Null comparers are not allowed.
			if (comparer == null)
			{
				throw new ArgumentNullException ();
			}
			
			// Null-value check
			if ((Object)dataItem == null)
			{
				for (int i = 0; i < _size; ++i)
				{
					if ((Object)_collection [i] == null) return true;
				}
			}
			else
			{
				for(int i=0; i<_size; ++i)
				{
					if(comparer.Equals(_collection[i], dataItem)) return true;
				}
			}

			return false;
		}

	}

}