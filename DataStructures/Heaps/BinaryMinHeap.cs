using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Heaps
{
    /// <summary>
    /// Minimum Heap Data Structure.
    /// </summary>
    public class BinaryMinHeap<T> : IMinHeap<T> where T : IComparable<T>
    {
        /// <summary>
        /// Instance Variables.
        /// _collection: The list of elements. Implemented as an array-based list with auto-resizing.
        /// </summary>
        private ArrayList<T> _collection { get; set; }
        private Comparer<T> _heapComparer = Comparer<T>.Default;


        /// <summary>
        /// CONSTRUCTORS
        /// </summary>
        public BinaryMinHeap() : this(0, null) { }
        public BinaryMinHeap(Comparer<T> comparer) : this(0, comparer) { }
        public BinaryMinHeap(int capacity, Comparer<T> comparer)
        {
            _collection = new ArrayList<T>(capacity);
            _heapComparer = comparer ?? Comparer<T>.Default;
        }


        /// <summary>
        /// Builds a min heap from the inner array-list _collection.
        /// </summary>
        private void _buildMinHeap()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for (int node = lastNodeWithChildren; node >= 0; node--)
            {
                _minHeapify(node, lastIndex);
            }
        }

        /// <summary>
        /// Private Method. Used to restore heap condition after insertion
        /// </summary>
        private void _siftUp(int nodeIndex)
        {
            int parent = (nodeIndex - 1) / 2;
            while (_heapComparer.Compare(_collection[nodeIndex], _collection[parent]) < 0)
            {
                _collection.Swap(parent, nodeIndex);
                nodeIndex = parent;
                parent = (nodeIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Private Method. Used in Building a Min Heap.
        /// </summary>
        /// <typeparam name="T">Type of Heap elements</typeparam>
        /// <param name="nodeIndex">The node index to heapify at.</param>
        /// <param name="lastIndex">The last index of collection to stop at.</param>
        private void _minHeapify(int nodeIndex, int lastIndex)
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
                _collection.Swap(nodeIndex, smallest);
                _minHeapify(smallest, lastIndex);
            }
        }


        /// <summary>
        /// Returns the number of elements in heap
        /// </summary>
        public int Count
        {
            get { return _collection.Count; }
        }

        /// <summary>
        /// Checks whether this heap is empty
        /// </summary>
        public bool IsEmpty
        {
            get { return (_collection.Count == 0); }
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
                    throw new IndexOutOfRangeException();
                }

                return _collection[index];
            }
            set
            {
                if (index < 0 || index >= this.Count)
                {
                    throw new IndexOutOfRangeException();
                }

                _collection[index] = value;

                if (index != 0 && _heapComparer.Compare(_collection[index], _collection[(index - 1) / 2]) < 0) // less than or equal to min
                    _siftUp(index);
                else
                    _minHeapify(index, _collection.Count - 1);
            }
        }

        /// <summary>
        /// Heapifies the specified newCollection. Overrides the current heap.
        /// </summary>
        /// <param name="newCollection">New collection.</param>
        public void Initialize(IList<T> newCollection)
        {
            if (newCollection.Count > 0)
            {
                // Reset and reserve the size of the newCollection
                _collection = new ArrayList<T>(newCollection.Count);

                // Copy the elements from the newCollection to the inner collection
                for (int i = 0; i < newCollection.Count; ++i)
                {
                    _collection.InsertAt(newCollection[i], i);
                }

                // Build the heap
                _buildMinHeap();
            }
        }

        /// <summary>
        /// Adding a new key to the heap.
        /// </summary>
        /// <param name="heapKey">Heap key.</param>
        public void Add(T heapKey)
        {
            _collection.Add(heapKey);
            if (!IsEmpty)
            {
                _siftUp(_collection.Count - 1);
            }
        }

        /// <summary>
        /// Find the minimum node of a min heap.
        /// </summary>
        /// <returns>The minimum.</returns>
        public T Peek()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            return _collection.First;
        }

        /// <summary>
        /// Removes the node of minimum value from a min heap.
        /// </summary>
        public void RemoveMin()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            int min = 0;
            int last = _collection.Count - 1;
            _collection.Swap(min, last);

            _collection.RemoveAt(last);
            last--;

            _minHeapify(0, last);
        }

        /// <summary>
        /// Returns the node of minimum value from a min heap after removing it from the heap.
        /// </summary>
        /// <returns>The min.</returns>
        public T ExtractMin()
        {
            var min = Peek();
            RemoveMin();
            return min;
        }

        /// <summary>
        /// Clear this heap.
        /// </summary>
        public void Clear()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            _collection.Clear();
        }

        /// <summary>
        /// Rebuilds the heap.
        /// </summary>
        public void RebuildHeap()
        {
            _buildMinHeap();
        }

        /// <summary>
        /// Returns an array version of this heap.
        /// </summary>
        public T[] ToArray()
        {
            return _collection.ToArray();
        }

        /// <summary>
        /// Returns a list version of this heap.
        /// </summary>
        public List<T> ToList()
        {
            return _collection.ToList();
        }

        /// <summary>
        /// Union two heaps together, returns a new min-heap of both heaps' elements, 
        /// ... and then destroys the original ones.
        /// </summary>
        public BinaryMinHeap<T> Union(ref BinaryMinHeap<T> firstMinHeap, ref BinaryMinHeap<T> secondMinHeap)
        {
            if (firstMinHeap == null || secondMinHeap == null)
                throw new ArgumentNullException("Null heaps are not allowed.");

            // Create a new heap with reserved size.
            int size = firstMinHeap.Count + secondMinHeap.Count;
            var newHeap = new BinaryMinHeap<T>(size, Comparer<T>.Default);

            // Insert into the new heap.
            while (firstMinHeap.IsEmpty == false)
                newHeap.Add(firstMinHeap.ExtractMin());

            while (secondMinHeap.IsEmpty == false)
                newHeap.Add(secondMinHeap.ExtractMin());

            // Destroy the two heaps.
            firstMinHeap = secondMinHeap = null;

            return newHeap;
        }

        /// <summary>
        /// Returns a new max heap that contains all elements of this heap.
        /// </summary>
        public IMaxHeap<T> ToMaxHeap()
        {
            BinaryMaxHeap<T> newMaxHeap = new BinaryMaxHeap<T>(this.Count, this._heapComparer);
            newMaxHeap.Initialize(this._collection.ToArray());
            return newMaxHeap;
        }

    }

}
