using System;
using System.Collections.Generic;

using DataStructures.Helpers;

namespace DataStructures
{
	/// <summary>
	/// Maximum Heap Data Structure.
	/// </summary>
	public class MaxHeap<T>
	{
		/// <summary>
		/// Instance Variables.
		/// _collection: The list of elements. Implemented as an array-based list with auto-resizing.
		/// </summary>
		private ArrayList<T> _collection { get; set; }
		private Comparer<T> _heapComparer = Comparer<T>.Default;


		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		public MaxHeap() : this(0, null) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="comparer">Heap keys comparer.</param>
		public MaxHeap(Comparer<T> comparer) : this(0, comparer) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="capacity">Heap capacity.</param>
		public MaxHeap(int capacity, Comparer<T> comparer)
		{
			_collection = new ArrayList<T>(capacity);
			_heapComparer = comparer ?? Comparer<T>.Default;
		}


		/// <summary>
		/// Private Method.
		/// Builds a max heap from the inner array-list _collection.
		/// </summary>
		private void BuildMaxHeap()
		{
			int lastIndex = _collection.Count - 1;
			int lastNodeWithChildren = (lastIndex / 2);

			for(int node = lastNodeWithChildren; node >= 0; node--)
			{
				MaxHeapify<T>(node, lastIndex);
			}
		}


		/// <summary>
		/// Private Method.
		/// Used in Building a Max Heap.
		/// </summary>
		/// <typeparam name="T">Type of Heap elements</typeparam>
		/// <param name="nodeIndex">The node index to heapify at.</param>
		/// <param name="lastIndex">The last index of collection to stop at.</param>
		private void MaxHeapify<T>(int nodeIndex, int lastIndex)
		{
			// assume that the subtrees left(node) and right(node) are max-heaps
			int left = (nodeIndex * 2) + 1;
			int right = left + 1;
			int largest = nodeIndex;

			// If collection[left] > collection[nodeIndex]
			if (left <= lastIndex && _heapComparer.Compare(_collection[left], _collection[nodeIndex]) > 0)
				largest = left;

			// If collection[right] > collection[largest]
			if (right <= lastIndex && _heapComparer.Compare(_collection[right], _collection[largest]) > 0)
				largest = right;

			// Swap and heapify
			if (largest != nodeIndex)
			{
				_collection.Swap(nodeIndex, largest);
				MaxHeapify<T>(largest, lastIndex);
			}
		}


		/// <summary>
		/// Returns the number of elements in heap
		/// </summary>
		public int Count
		{
			get
			{
				return _collection.Count;
			}
		}


		/// <summary>
		/// Checks whether this heap is empty
		/// </summary>
		public bool IsEmpty
		{
			get
			{
				return (_collection.Count == 0);
			}
		}


		/// <summary>
		/// Gets or sets the at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public T this[int index]
		{
			get
			{
				if (index < 0 || index > this.Count || this.Count == 0)
				{
					throw new IndexOutOfRangeException ();
				}

				return _collection [index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException ();
				}

				_collection [index] = value;

				if(_heapComparer.Compare(_collection[index], _collection[0]) >= 0) // greater than or equal to max
				{
					_collection.Swap (0, index);
					BuildMaxHeap ();
				}
			}
		}


		/// <summary>
		/// Heapifies the specified newCollection. Overrides the current heap.
		/// </summary>
		/// <param name="newCollection">New collection.</param>
		public void Heapify(IList<T> newCollection)
		{
			if (newCollection.Count > 0)
			{
				// Reset and reserve the size of the newCollection
				_collection = new ArrayList<T> (newCollection.Count);

				// Copy the elements from the newCollection to the inner collection
				for (int i = 0; i < newCollection.Count; ++i)
				{
					_collection.InsertAt (newCollection [i], i);
				}

				// Build the heap
				BuildMaxHeap();
			}
		}


		/// <summary>
		/// Adding a new key to the heap.
		/// </summary>
		/// <param name="heapKey">Heap key.</param>
		public void Insert(T heapKey)
		{
			if(IsEmpty)
			{
				_collection.Add(heapKey);
			}
			else
			{
				_collection.Add(heapKey);
				BuildMaxHeap ();
			}
		}


		/// <summary>
		/// Find the maximum node of a max heap.
		/// </summary>
		/// <returns>The maximum.</returns>
		public T Peek()
		{
			if (IsEmpty)
			{
				throw new Exception ("Heap is empty.");
			}
			
			return _collection.First;
		}


		/// <summary>
		/// Remove a key from the heap.
		/// </summary>
		/// <param name="heapKey">Heap key.</param>
		public void Remove(T heapKey)
		{
			if (!IsEmpty)
			{
				int last = _collection.Count - 1;
				int index = _collection.IndexOf(heapKey);
				_collection.Swap (index, last);

				_collection.RemoveAt (last);
				last--;

				MaxHeapify<T>(0, last);
			}
		}


		/// <summary>
		/// Removes the key at index.
		/// </summary>
		/// <param name="index">Index.</param>
		public void RemoveAt(int index)
		{
			if (!IsEmpty)
			{
				int last = _collection.Count - 1;
				_collection.Swap (index, last);

				_collection.RemoveAt (last);
				last--;

				MaxHeapify<T>(0, last);
			}
		}


		/// <summary>
		/// Removes all keys that match the predicate.
		/// </summary>
		/// <param name="searchMatch">Search match.</param>
		public void RemoveAll(Predicate<T> searchMatch)
		{
			for (int i = 0; i < _collection.Count; ++i)
			{
				if (searchMatch (_collection [i]))
				{
					RemoveAt (i);
				}
			}
		}


		/// <summary>
		/// Removes the node of minimum value from a min heap.
		/// </summary>
		public void RemoveMax()
		{
			if (!IsEmpty)
			{
				int max = 0;
				int last = _collection.Count - 1;
				_collection.Swap (max, last);

				_collection.RemoveAt (last);
				last--;

				MaxHeapify<T>(0, last);
			}
		}


		/// <summary>
		/// Returns the node of maximum value from a max heap after removing it from the heap.
		/// </summary>
		/// <returns>The max.</returns>
		public T ExtractMax()
		{
			var max = Peek ();
			RemoveMax();
			return max;
		}


		/// <summary>
		/// Clear this heap.
		/// </summary>
		public void Clear()
		{
			if (!IsEmpty)
			{
				_collection.Clear ();
			}
		}


		/// <summary>
		/// Returns an array version of this heap.
		/// </summary>
		/// <returns>The array.</returns>
		public T[] ToArray()
		{
			return _collection.ToArray ();
		}


		/// <summary>
		/// Returns a list version of this heap.
		/// </summary>
		/// <returns>The list.</returns>
		public List<T> ToList()
		{
			return _collection.ToList ();
		}


		/// <summary>
		/// Returns a new min heap that contains all elements of this heap.
		/// </summary>
		/// <returns>The minimum heap.</returns>
		public MinHeap<T> ToMinHeap()
		{
			MinHeap<T> newMinHeap = new MinHeap<T> (this.Count, this._heapComparer);
			newMinHeap.Heapify(this._collection.ToArray());
			return newMinHeap;
		}

	}

}

