using System;
using System.Collections.Generic;

using DataStructures.Common;
using DataStructures.Lists;

namespace DataStructures.Heaps
{
    /// <summary>
    /// Maximum Heap Data Structure.
    /// </summary>
    public class BinaryMaxHeap<T> : IMaxHeap<T> where T : IComparable<T>
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
        public BinaryMaxHeap() : this(0, null) { }
        public BinaryMaxHeap(Comparer<T> comparer) : this(0, comparer) { }
        public BinaryMaxHeap(int capacity, Comparer<T> comparer)
        {
            _collection = new ArrayList<T>(capacity);
            _heapComparer = comparer ?? Comparer<T>.Default;
        }

        /// <summary>
        /// Private Method. Builds a max heap from the inner array-list _collection.
        /// </summary>
        private void _buildMaxHeap()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for (int node = lastNodeWithChildren; node >= 0; node--)
            {
                _maxHeapify(node, lastIndex);
            }
        }

        /// <summary>
        /// Private Method. Used to restore heap condition after insertion
        /// </summary>
        private void _siftUp(int nodeIndex)
        {
            int parent = (nodeIndex - 1) / 2;
            while (_heapComparer.Compare(_collection[nodeIndex], _collection[parent]) > 0)
            {
                _collection.Swap(parent, nodeIndex);
                nodeIndex = parent;
                parent = (nodeIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Private Method. Used in Building a Max Heap.
        /// </summary>
        private void _maxHeapify(int nodeIndex, int lastIndex)
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
                _maxHeapify(largest, lastIndex);
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

                if (index != 0 && _heapComparer.Compare(_collection[index], _collection[(index - 1) / 2]) > 0) // greater than or equal to max
                    _siftUp(index);
                else
                    _maxHeapify(index, _collection.Count - 1);
            }
        }

        /// <summary>
        /// Heapifies the specified newCollection. Overrides the current heap.
        /// </summary>
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
                _buildMaxHeap();
            }
        }

        /// <summary>
        /// Adding a new key to the heap.
        /// </summary>
        public void Add(T heapKey)
        {
            _collection.Add(heapKey);
            if (!IsEmpty)
            {
                _siftUp(_collection.Count - 1);
            }
        }

        /// <summary>
        /// Find the maximum node of a max heap.
        /// </summary>
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
        public void RemoveMax()
        {
            if (IsEmpty)
            {
                throw new Exception("Heap is empty.");
            }

            int max = 0;
            int last = _collection.Count - 1;
            _collection.Swap(max, last);

            _collection.RemoveAt(last);
            last--;

            _maxHeapify(0, last);
        }

        /// <summary>
        /// Returns the node of maximum value from a max heap after removing it from the heap.
        /// </summary>
        public T ExtractMax()
        {
            var max = Peek();
            RemoveMax();
            return max;
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
            _buildMaxHeap();
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
        public BinaryMaxHeap<T> Union(ref BinaryMaxHeap<T> firstMaxHeap, ref BinaryMaxHeap<T> secondMaxHeap)
        {
            if (firstMaxHeap == null || secondMaxHeap == null)
                throw new ArgumentNullException("Null heaps are not allowed.");

            // Create a new heap with reserved size.
            int size = firstMaxHeap.Count + secondMaxHeap.Count;
            var newHeap = new BinaryMaxHeap<T>(size, Comparer<T>.Default);

            // Insert into the new heap.
            while (firstMaxHeap.IsEmpty == false)
                newHeap.Add(firstMaxHeap.ExtractMax());

            while (secondMaxHeap.IsEmpty == false)
                newHeap.Add(secondMaxHeap.ExtractMax());

            // Destroy the two heaps.
            firstMaxHeap = secondMaxHeap = null;

            return newHeap;
        }

        /// <summary>
        /// Returns a new min heap that contains all elements of this heap.
        /// </summary>
        public IMinHeap<T> ToMinHeap()
        {
            BinaryMinHeap<T> newMinHeap = new BinaryMinHeap<T>(this.Count, this._heapComparer);
            newMinHeap.Initialize(this._collection.ToArray());
            return newMinHeap;
        }

    }

}

