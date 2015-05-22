using System;
using System.Collections.Generic;

using DataStructures.Interfaces;
using DataStructures.Helpers;

namespace DataStructures
{
    /// <summary>
    /// Minimum Heap Data Structure.
    /// </summary>
    public class MinHeap<T> : IMinHeap<T> where T : IComparable<T>
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
        /// <param name="heapType">Heap type, max or min.</param>
        public MinHeap() : this(0, null) { }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="comparer">Heap keys comparer.</param>
        public MinHeap(Comparer<T> comparer) : this(0, comparer) { }

        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="capacity">Heap capacity.</param>
        public MinHeap(int capacity, Comparer<T> comparer)
        {
            _collection = new ArrayList<T>(capacity);
            _heapComparer = comparer ?? Comparer<T>.Default;
        }


        /// <summary>
        /// Builds a min heap from the inner array-list _collection.
        /// </summary>
        private void BuildMinHeap()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for (int node = lastNodeWithChildren; node >= 0; node--)
            {
                MinHeapify<T>(node, lastIndex);
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
                _collection.Swap(nodeIndex, smallest);
                MinHeapify<T>(smallest, lastIndex);
            }
        }


        /// <summary>
        /// Returns the number of elements in heap
        /// </summary>
        public int Count()
        {
            return _collection.Count;
        }


        /// <summary>
        /// Checks whether this heap is empty
        /// </summary>
        public bool IsEmpty()
        {
            return (_collection.Count == 0);
        }


        /// <summary>
        /// Gets or sets the at the specified index.
        /// </summary>
        /// <param name="index">Index.</param>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count() || this.Count() == 0)
                {
                    throw new IndexOutOfRangeException();
                }

                return _collection[index];
            }
            set
            {
                if (index < 0 || index >= this.Count())
                {
                    throw new IndexOutOfRangeException();
                }

                _collection[index] = value;

                if (_heapComparer.Compare(_collection[index], _collection[0]) <= 0) // less than or equal to min
                {
                    _collection.Swap(0, index);
                    BuildMinHeap();
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
                _collection = new ArrayList<T>(newCollection.Count);

                // Copy the elements from the newCollection to the inner collection
                for (int i = 0; i < newCollection.Count; ++i)
                {
                    _collection.InsertAt(newCollection[i], i);
                }

                // Build the heap
                BuildMinHeap();
            }
        }


        /// <summary>
        /// Adding a new key to the heap.
        /// </summary>
        /// <param name="heapKey">Heap key.</param>
        public void Insert(T heapKey)
        {
            if (IsEmpty())
            {
                _collection.Add(heapKey);
            }
            else
            {
                _collection.Add(heapKey);
                BuildMinHeap();
            }
        }


        /// <summary>
        /// Find the minimum node of a min heap.
        /// </summary>
        /// <returns>The minimum.</returns>
        public T Peek()
        {
            if (IsEmpty())
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
            if (IsEmpty())
            {
                throw new Exception("Heap is empty.");
            }

            int min = 0;
            int last = _collection.Count - 1;
            _collection.Swap(min, last);

            _collection.RemoveAt(last);
            last--;

            MinHeapify<T>(0, last);
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
            if (IsEmpty())
            {
                throw new Exception("Heap is empty.");
            }

            _collection.Clear();
        }


        /// <summary>
        /// Returns an array version of this heap.
        /// </summary>
        /// <returns>The array.</returns>
        public T[] ToArray()
        {
            return _collection.ToArray();
        }


        /// <summary>
        /// Returns a list version of this heap.
        /// </summary>
        /// <returns>The list.</returns>
        public List<T> ToList()
        {
            return _collection.ToList();
        }


        /// <summary>
        /// Returns a new max heap that contains all elements of this heap.
        /// </summary>
        /// <returns>The max heap.</returns>
        public MaxHeap<T> ToMaxHeap()
        {
            MaxHeap<T> newMaxHeap = new MaxHeap<T>(this.Count(), this._heapComparer);
            newMaxHeap.Heapify(this._collection.ToArray());
            return newMaxHeap;
        }


        ///// <summary>
        ///// Remove a key from the heap.
        ///// </summary>
        ///// <param name="heapKey">Heap key.</param>
        //public void Remove(T heapKey)
        //{
        //    if (!IsEmpty)
        //    {
        //        int last = _collection.Count - 1;
        //        int index = _collection.IndexOf(heapKey);
        //        _collection.Swap (index, last);
        //        _collection.RemoveAt (last);
        //        last--;
        //        MinHeapify<T>(0, last);
        //    }
        //}


        ///// <summary>
        ///// Removes the key at index.
        ///// </summary>
        ///// <param name="index">Index.</param>
        //public void RemoveAt(int index)
        //{
        //    if (!IsEmpty)
        //    {
        //        int last = _collection.Count - 1;
        //        _collection.Swap (index, last);
        //        _collection.RemoveAt (last);
        //        last--;
        //        MinHeapify<T>(0, last);
        //    }
        //}


        ///// <summary>
        ///// Removes all keys that match the predicate.
        ///// </summary>
        ///// <param name="searchMatch">Search match.</param>
        //public void RemoveAll(Predicate<T> searchMatch)
        //{
        //    for (int i = 0; i < _collection.Count; ++i)
        //    {
        //        if (searchMatch (_collection [i]))
        //        {
        //            RemoveAt (i);
        //        }
        //    }
        //}

    }

}
