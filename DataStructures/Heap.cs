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
        /// CONSTRUCTORS
        /// </summary>
        public Heap() : this(0, HeapType.MaxHeap) { }

        public Heap(int capacity, HeapType heapType)
        {
            _collection = new ArrayList<T>(capacity);
            _heapType = heapType;
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
        /// Checks whether the Heap is a MaxHeap
        /// </summary>
        public bool IsMaxHeap
        {
            get
            {
                return (_heapType == HeapType.MaxHeap);
            }
        }


        /// <summary>
        /// Checks whether the Heap is a MinHeap
        /// </summary>
        public bool IsMinHeap
        {
            get
            {
                return (_heapType == HeapType.MinHeap);
            }
        }


        /// <summary>
        /// Builds a Minimum Heap from the inner _collection ArrayList.
        /// </summary>
        public void BuildMinHeap<T>()
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
        /// Builds a Maximum Heap from the inner _collection ArrayList.
        /// </summary>
        public void BuildMaxHeap<T>()
        {
            int lastIndex = _collection.Count - 1;
            int lastNodeWithChildren = (lastIndex / 2);

            for(int node = lastNodeWithChildren; node >= 0; node--)
            {
                MaxHeapify<T>(node, lastIndex);
            }

            _heapType = HeapType.MaxHeap;
        }


        public void Insert(T heapKey)
        {
            if(IsEmpty)
            {
                _collection.Add(heapKey);
            }
            else
            {
                _collection.Add(heapKey);

                // if the new element < top
                if (_heapComparer.Compare(heapKey, _collection.First) < 0)

            }
        }


        /// <summary>
        /// Removes the minimum element from a MinHeap
        /// </summary>
        public void RemoveMin()
        {
            if (IsMinHeap)
            {
                _collection.RemoveAt(0);
            }
            else
            {
                throw new ArgumentException("This is a MAX HEAP.");
            }
        }
        

        /// <summary>
        /// Removes the maximum element from a MaxHeap
        /// </summary>
        public void RemoveMax()
        {
            if(IsMaxHeap)
            {
                _collection.RemoveAt(0);
            }
            else
            {
                throw new ArgumentException("This is a MIN HEAP.");
            }
        }

    }

}
