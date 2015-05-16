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
		/// For each element in list, apply the specified action to it.
		/// </summary>
		/// <param name="action">Typed Action.</param>
		public void ForEach(Action<T> action)
		{
			// Null actions are not allowed.
			if (action == null)
			{
				throw new ArgumentNullException ();
			}

			for (int i = 0; i < _size; ++i)
			{
				action (_collection [i]);
			}
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
		/// <returns>True if list contains the dataItem, false otherwise.</returns>
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
		/// <returns>True if list contains the dataItem, false otherwise.</returns>
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


		/// <summary>
		/// Checks whether an item specified via a Predicate<T> function exists exists in list.
		/// </summary>
		/// <param name="searchMatch">Match predicate.</param>
		public bool Exists(Predicate<T> searchMatch)
		{
			// Use the FindIndex to look through the collection
			// If the returned index != -1 then it does exist, otherwise it doesn't.
			return (FindIndex (searchMatch) != -1);
		}


		/// <summary>
		/// Finds the index of the element that matches the predicate.
		/// </summary>
		/// <returns>The index of element if found, -1 otherwise.</returns>
		/// <param name="searchMatch">Match predicate.</param>
		public int FindIndex(Predicate<T> searchMatch)
		{
			return FindIndex (0, _size, searchMatch);
		}


		/// <summary>
		/// Finds the index of the element that matches the predicate.
		/// </summary>
		/// <returns>The index of the element if found, -1 otherwise.</returns>
		/// <param name="startIndex">Starting index to search from.</param>
		/// <param name="searchMatch">Match predicate.</param>
		public int FindIndex(int startIndex, Predicate<T> searchMatch)
		{
			return FindIndex (startIndex, (_size - startIndex), searchMatch);
		}


		/// <summary>
		/// Finds the index of the first element that matches the given predicate function.
		/// </summary>
		/// <returns>The index of element if found, -1 if not found.</returns>
		/// <param name="startIndex">Starting index of search.</param>
		/// <param name="count">Count of elements to search through.</param>
		/// <param name="searchMatch">Match predicate function.</param>
		public int FindIndex(int startIndex, int count, Predicate<T> searchMatch)
		{
			// Check bound of startIndex
			if ((uint)startIndex > (uint)_size)
			{
				throw new ArgumentOutOfRangeException ();
			}

			// CHeck the bounds of count and startIndex with respect to _size
			if (count < 0 || startIndex > (_size - count))
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Null match-predicates are not allowed
			if (searchMatch == null)
			{
				throw new ArgumentNullException ();
			}

			// Start the search
			int endIndex = startIndex + count;
			for (int index = startIndex; index < endIndex; ++index)
			{
				if (searchMatch (_collection [index]) == true) return index;
			}

			// Not found, return -1
			return -1;
		}

	}

}