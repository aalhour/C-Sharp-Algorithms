using System;
using System.Collections.Generic;

namespace DataStructures
{
    public class Heap<T>
    {
        /// <summary>
        /// The type of heaps enum
        /// </summary>
        public enum HeapType
        {
            MaxHeap = 0,
            MinHeap = 1
        }

        /// <summary>
        /// Instance Variables.
        /// _collection: The list of elements. Implemented as an array-based list with auto-resizing.
        /// </summary>
        private HeapType _heapType { get; set; }
        private ArrayList<T> _collection { get; set; }
        private Comparer<T> _heapComparer = Comparer<T>.Default;


        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="heapType">Heap type, max or min.</param>
		public Heap(HeapType heapType) : this(0, heapType, null) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="heapType">Heap type, max or min.</param>
		/// <param name="comparer">Heap keys comparer.</param>
		public Heap(HeapType heapType, Comparer<T> comparer) : this(0, heapType, comparer) { }

		/// <summary>
		/// CONSTRUCTOR
		/// </summary>
		/// <param name="capacity">Heap capacity.</param>
		/// <param name="heapType">Heap type, max or min.</param>
		public Heap(int capacity, HeapType heapType, Comparer<T> comparer)
        {
            _collection = new ArrayList<T>(capacity);
            _heapType = heapType;
			_heapComparer = comparer ?? Comparer<T>.Default;
        }


        /// <summary>
        /// Swaps two elements in the inner _collection.
        /// </summary>
        /// <param name="firstIndex">Index of first element</param>
        /// <param name="secondIndex">Index of second element</param>
        private void Swap(int firstIndex, int secondIndex)
        {
            var tempVar = _collection[firstIndex];
            _collection[firstIndex] = _collection[secondIndex];
            _collection[secondIndex] = tempVar;
        }


        /// <summary>
        /// Private Method. Used in Building a Max Heap.
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
                Swap(nodeIndex, largest);
                MaxHeapify<T>(largest, lastIndex);
            }
        }


        /// <summary>
        /// Private Method. Used in Building a Min Heap.
        /// </summary>
        /// <typeparam name="T">Type of Heap elements</typeparam>
        /// <param name="nodeIndex">The node index to heapify at.</param>
        /// <param name="lastIndex">The last index of collection to stop at.</param>
        private void MinHeapify<T>(int nodeIndex, int lastIndex)
        {
            // assume that the subtrees left(node) and right(node) are max-heaps
            int left = (nodeIndex * 2) + 1;
            int right = left + 1;
            int smallest = nodeIndex;

            // If collection[left] < collection[nodeIndex]
            if (left <= lastIndex && _heapComparer.Compare(_collection[left], _collection[nodeIndex]) < 0)
                smallest = left;

            // If collection[right] < collection[smallest]
            if (right <= lastIndex && _heapComparer.Compare(_collection[right], _collection[smallest]) < 0)
                smallest = right;

            // Swap and heapify
            if (smallest != nodeIndex)
            {
                Swap(nodeIndex, smallest);
                MaxHeapify<T>(smallest, lastIndex);
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
		/// Checks whether the heap is a min heap
		/// </summary>
		public bool IsMinHeap
		{
			get
			{
				return (_heapType == HeapType.MinHeap);
			}
		}


		/// <summary>
		/// Checks whether the heap is a max heap
        /// </summary>
        public bool IsMaxHeap
        {
            get
            {
                return (_heapType == HeapType.MaxHeap);
            }
        }


        /// <summary>
		/// Builds a min heap from the inner array-list _collection.
        /// </summary>
        public void BuildMinHeap()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for (int node = lastNodeWithChildren; node >= 0; node--)
            {
                MinHeapify<T>(node, lastIndex);
            }

            _heapType = HeapType.MinHeap;
        }


        /// <summary>
        /// Builds a max heap from the inner array-list _collection.
        /// </summary>
        public void BuildMaxHeap()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for(int node = lastNodeWithChildren; node >= 0; node--)
            {
                MaxHeapify<T>(node, lastIndex);
            }

            _heapType = HeapType.MaxHeap;
        }


		/// <summary>
		/// Heapifies the specified newCollection.
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
				if (IsMaxHeap)
					BuildMaxHeap;
				else
					BuildMinHeap;
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

				if (IsMaxHeap && _heapComparer.Compare (heapKey, _collection.First) > 0) //heapKey > topElement
				{
					BuildMaxHeap ();
				}
				else if (IsMinHeap && _heapComparer.Compare (heapKey, _collection.First) < 0) //heapKey < topElement
				{
					BuildMinHeap ();
				}
            }
        }


		/// <summary>
		/// Find the maximum node of a max heap.
		/// </summary>
		/// <returns>The maximum.</returns>
		public T FindMax()
		{
			if (!IsMaxHeap)
			{
				throw new ArgumentException("This is a min-heap.");
			}

			try
			{
				return _collection.First;
			}
			catch(IndexOutOfRangeException)
			{
				throw new Exception ("Heap is empty.");
			}
		}


		/// <summary>
		/// Find the minimum node of a min heap.
		/// </summary>
		/// <returns>The minimum.</returns>
		public T FindMin()
		{
			if (!IsMinHeap)
			{
				throw new ArgumentException("This is a max-heap.");
			}

			try
			{
				return _collection.First;
			}
			catch(IndexOutOfRangeException)
			{
				throw new Exception ("Heap is empty.");
			}
		}


        /// <summary>
		/// Removes the node of minimum value from a min heap.
        /// </summary>
        public void RemoveMin()
        {
            if (!IsMinHeap)
            {
				throw new ArgumentException("This is a max-heap.");
            }
            
			if (!IsEmpty)
			{
				int min = 0;
				int last = _collection.Count - 1;

				Swap (min, last);

				_collection.RemoveAt (last);
			}
        }
        

        /// <summary>
        /// Removes the node of maximum value from a max heap.
        /// </summary>
        public void RemoveMax()
        {
            if(!IsMaxHeap)
            {
                throw new ArgumentException("This is a min-heap.");
            }

			if (!IsEmpty)
			{
				int max = 0;
				int last = _collection.Count - 1;

				Swap (max, last);

				_collection.RemoveAt (last);
			}
        }


		/// <summary>
		/// Returns the node of minimum value from a min heap after removing it from the heap.
		/// </summary>
		/// <returns>The min.</returns>
		public T ExtractMin()
		{
			if (!IsMinHeap)
			{
				throw new ArgumentException("This is a max-heap.");
			}

			var min = FindMin ();
			RemoveMin ();
			return min;
		}


		/// <summary>
		/// Returns the node of maximum value from a max heap after removing it from the heap.
		/// </summary>
		/// <returns>The max.</returns>
		public T ExtractMax()
		{
			if (!IsMaxHeap)
			{
				throw new ArgumentException("This is a min-heap.");
			}

			var max = FindMax();
			RemoveMax();
			return max;
		}

    }

}
