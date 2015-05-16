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
		/// Inserts a new element at an index. Doesn't override the cell at index.
		/// </summary>
		/// <param name="dataItem">Data item to insert.</param>
		/// <param name="index">Index of insertion.</param>
		public void InsertAt(T dataItem, int index)
		{
			if (index < 0 || (uint)index > (uint)_size)
			{
				throw new ArgumentOutOfRangeException ();
			}

			// If the inner array is full and there are no extra spaces, 
			// ... then maximize it's capacity to a minimum of _size + 1.
			if (_size == _collection.Length)
			{
				EnsureCapacity(_size + 1);
			}

			// If the index is not "at the end", then copy the elements of the array
			// ... between the specified index and the last index to the new range (index + 1, _size);
			// The cell at "index" will become available.
			if (index < _size)
			{
				Array.Copy (_collection, index, _collection, index + 1, (_size - index));
			}

			// Write the dataItem to the available cell.
			_collection[index] = dataItem;

			// Increase the size.
			_size++;
		}


		/// <summary>
		/// Removes the specified dataItem from list.
		/// </summary>
		/// <returns>>True if removed successfully, false otherwise.</returns>
		/// <param name="dataItem">Data item.</param>
		public bool Remove(T dataItem)
		{
			int index = IndexOf (dataItem);

			if (index >= 0)
			{
				RemoveAt (index);
				return true;
			}

			return false;
		}


		/// <summary>
		/// Removes the list element at the specified index.
		/// </summary>
		/// <param name="index">Index of element.</param>
		public void RemoveAt(int index)
		{
			if (index < 0 || (uint)index >= (uint)_size)
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Decrease the size by 1, to avoid doing Array.Copy if the element is to be removed from the tail of list. 
			this._size--;

			// If the index is still less than size, perform an Array.Copy to override the cell at index.
			// This operation is O(N), where N = size - index.
			if (index < _size)
			{
				Array.Copy (_collection, index + 1, _collection, index, (_size - index));
			}

			// Reset the writable cell to the default value of type T.
			_collection [_size] = default(T);
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
		/// Reverses this list.
		/// </summary>
		public void Reverse()
		{
			Reverse (0, _size);
		}


		/// <summary>
		/// Reverses the order of a number of elements. Starting a specific index.
		/// </summary>
		/// <param name="startIndex">Start index.</param>
		/// <param name="count">Count of elements to reverse.</param>
		public void Reverse(int startIndex, int count)
		{
			// Handle the bounds of startIndex
			if (startIndex < 0 || (uint)startIndex >= (uint)_size)
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Handle the bounds of count and startIndex with respect to _size.
			if (count < 0 || startIndex > (_size - count))
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Use Array.Reverse
			// Running complexity is better than O(N). But unknown.
			// Array.Reverse uses the closed-source function TrySZReverse.
			Array.Reverse (_collection, startIndex, count);
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
			if (startIndex < 0 || (uint)startIndex > (uint)_size)
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


		/// <summary>
		/// Returns the index of a given dataItem.
		/// </summary>
		/// <returns>Index of element in list.</returns>
		/// <param name="dataItem">Data item.</param>
		public int IndexOf(T dataItem)
		{
			return IndexOf (dataItem, 0, _size);
		}


		/// <summary>
		/// Returns the index of a given dataItem.
		/// </summary>
		/// <returns>Index of element in list.</returns>
		/// <param name="dataItem">Data item.</param>
		/// <param name="startIndex">The starting index to search from.</param>
		public int IndexOf(T dataItem, int startIndex)
		{
			return IndexOf (dataItem, startIndex, _size);
		}


		/// <summary>
		/// Returns the index of a given dataItem.
		/// </summary>
		/// <returns>Index of element in list.</returns>
		/// <param name="dataItem">Data item.</param>
		/// <param name="startIndex">The starting index to search from.</param>
		/// <param name="count">Count of elements to search through.</param>
		public int IndexOf(T dataItem, int startIndex, int count)
		{
			// Check the bound of the starting index.
			if (startIndex < 0 || (uint)startIndex > (uint)_size)
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Check the bounds of count and starting index with respect to _size.
			if (count < 0 || startIndex > (_size - count)) 
			{
				throw new ArgumentOutOfRangeException ();
			}

			// Everything is cool, start looking for the index
			// Use the Array.IndexOf
			// Array.IndexOf has a O(n) running time complexity, where: "n = count - size".
			// Array.IndexOf uses EqualityComparer<T>.Default to return the index of element which loops
			// ... over all the elements in the range [startIndex,count) in the array.
			return Array.IndexOf(_collection, dataItem, startIndex, count);
		}


		/// <summary>
		/// Find the specified element that matches the Search Predication.
		/// </summary>
		/// <param name="searchMatch">Match predicate.</param>
		public T Find(Predicate<T> searchMatch)
		{
			// Null Predicate functions are not allowed. 
			if (searchMatch == null)
			{
				throw new ArgumentNullException ();
			}

			// Begin searching, and return the matched element
			for (int i = 0; i < _size; ++i) 
			{
				if (searchMatch (_collection [i]))
				{
					return _collection [i];
				}
			}

			// Not found, return the default value of the type T.
			return default(T);
		}


		/// <summary>
		/// Finds all the elements that match the typed Search Predicate.
		/// </summary>
		/// <returns>ArrayList<T> of matched elements. Empty list is returned if not element was found.</returns>
		/// <param name="searchMatch">Match predicate.</param>
		public ArrayList<T> FindAll(Predicate<T> searchMatch)
		{
			// Null Predicate functions are not allowed. 
			if (searchMatch == null)
			{
				throw new ArgumentNullException ();
			}

			ArrayList<T> matchedElements = new ArrayList<T> ();

			// Begin searching, and add the matched elements to the new list.
			for (int i = 0; i < _size; ++i) 
			{
				if (searchMatch (_collection [i]))
				{
					matchedElements.Add(_collection [i]);
				}
			}

			// Return the new list of elements.
			return matchedElements;
		}
	}

}